using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableInstantiator : MonoBehaviour
{
    [SerializeField] AssetReferenceGameObject prefabReference;
    [SerializeField] Transform testObject;
    
    
    public async void Update()
    {
        testObject.Rotate(0, 70f * Time.deltaTime, 0);
        if (Input.GetKeyDown(KeyCode.I))
        {
            testObject.localScale = new Vector3(2, 2, 2);
            // prefabReference.InstantiateAsync();

            prefabReference.LoadAssetAsync().Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    testObject.localScale = new Vector3(3, 3, 3);

                    Instantiate(handle.Result);
                }
                else
                {
                    testObject.localScale = new Vector3(0, 0, 0);

                    Debug.LogError("Failed to load asset");
                }
            };
        }
    }
}