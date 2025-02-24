using UnityEngine;
using UnityEngine.SceneManagement;

public class SkiipLevel : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
             

        LoadNextScene();
        }
    }
    private void LoadNextScene()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }
}

