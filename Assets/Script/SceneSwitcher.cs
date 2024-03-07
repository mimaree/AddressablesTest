using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    AssetReference sceneReferenceNext;
    
    public bool isAddressable = false;
    
    void Update()
    {
        // Przechodź do następnej sceny na prawy przycisk myszy
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 1 oznacza prawy przycisk myszy
        {
            LoadNextScene();
        }

        // Przechodź do poprzedniej sceny na lewy przycisk myszy
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 0 oznacza lewy przycisk myszy
        {
            LoadPreviousScene();
        }
    }

    void LoadNextScene()
    {
        if(isAddressable)
        {
            sceneReferenceNext.LoadSceneAsync();
            return;
        }
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = (currentSceneIndex - 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(previousSceneIndex);
    }
}