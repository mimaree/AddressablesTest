using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

public class AddresablesUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI updateCatalogCount;
    [SerializeField] TextMeshProUGUI updateAddressablesCount;
    [SerializeField] TextMeshProUGUI downloadSizeLabelA;
    [SerializeField] TextMeshProUGUI downloadSizeLabelB;
    
    
    [SerializeField] TextMeshProUGUI caltalogUpdate;
    
    [SerializeField] TextMeshProUGUI LoadABar;
    [SerializeField] TextMeshProUGUI LoadBBar;
    
    Coroutine downloadLabelCoroutineA;
    Coroutine downloadLabelCoroutineB;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CheckForUpdates();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            CatalogUpdate();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            downloadLabelCoroutineA = StartCoroutine(DownloadLabel("A", LoadABar));
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            downloadLabelCoroutineB =  StartCoroutine(DownloadLabel("B", LoadBBar));
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAllData();
        }
    }

    public async void CatalogUpdate()
    {
        Addressables.CheckForCatalogUpdates(false).Completed += updateCheck =>
        {
            if (updateCheck.Status == AsyncOperationStatus.Succeeded)
            {
                caltalogUpdate.text = "updateCatalogCount " + updateCheck.Result.Count.ToString();
            }
            else
            {
                caltalogUpdate.text = "Failed to get download size";
            }
        };
    }

    public async void CheckForUpdates()
    {
        Addressables.GetDownloadSizeAsync("A").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                downloadSizeLabelA.text = "Label A Download size: " + handle.Result;
            }
            else
            {
                downloadSizeLabelA.text = "Failed to get download size";
            }
        };

        Addressables.GetDownloadSizeAsync("B").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                downloadSizeLabelB.text = "Label B Download size:" + handle.Result;
            }
            else
            {
                downloadSizeLabelB.text = "Failed to get download size";
            }
        };

        
    }
    
    void ResetAllData()
    {
        string path = Application.persistentDataPath;

        DirectoryInfo directory = new DirectoryInfo(path);

        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete(); 
        }

        foreach (DirectoryInfo dir in directory.GetDirectories())
        {
            dir.Delete(true); 
        }

        Debug.Log("Wszystkie dane zostały usunięte.");
    }

    IEnumerator DownloadLabel(string label, TextMeshProUGUI loadBar)
    {
        var downloadHandle = Addressables.DownloadDependenciesAsync(label, false);
        float progress = 0;

        while (downloadHandle.Status == AsyncOperationStatus.None)
        {
            float percentageComplete = downloadHandle.GetDownloadStatus().Percent;
            if (percentageComplete > progress * 1.1) // Report at most every 10% or so
            {
                progress = percentageComplete; // More accurate %
                loadBar.text = $"Load label {label}: " + progress.ToString();

            }
            yield return null;
        }
        loadBar.text = $"Load label {label}:" + progress.ToString() + " is Succeeded: " + $"{downloadHandle.Status == AsyncOperationStatus.Succeeded}";
        Addressables.Release(downloadHandle); //Release the operation handle
    }
    
    public async Task LoadAddressableAsset(string addressableKey)
    {
        // Załadowanie zasobu o danym kluczu; jeśli zasób został zaktualizowany, pobierze najnowszą wersję
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(addressableKey);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // Zasób został załadowany, tutaj można go użyć (np. zainstancjonować)
            GameObject asset = handle.Result;
        }
    }
}