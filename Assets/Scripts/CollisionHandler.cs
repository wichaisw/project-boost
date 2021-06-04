using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Friendly":
                Debug.Log("collide Friendly");
                break;
            case "Fuel":
                Debug.Log("collide Fuel");
                break;
            case "Finish":
                Debug.Log("collide Finish");
                LoadNextLevel();
                break;
            default:
                Debug.Log("Crashed!");
                StartCrashSequence();
                break;
        }
    }
    
    void StartCrashSequence() 
    {
        Movement movement = GetComponent<Movement>();
        movement.rocketSound.Stop(); 
        if(!movement.gameOverSound.isPlaying) 
        {
            movement.gameOverSound.Play();
        }
        movement.enabled = false;
        
        Invoke("ReloadLevel", 1.5f);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
