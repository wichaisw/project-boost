using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    Movement movement;
    bool isStageCleared = false;
    bool isCrashed = false;
    [SerializeField] float stageClearDelay = 3f;
    [SerializeField] float crashDelay = 1.5f;
    void Start()
    {   
        isStageCleared = false;
        isCrashed = false;
        movement = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Friendly":
                Debug.Log("collide Friendly");
                break;
            case "Fuel":
                Debug.Log("collide Fuel");
                break;
            case "Finish":
                if(!isCrashed) 
                {   
                    isStageCleared = true;
                    Debug.Log("collide Finish");
                    StartStageClearSequence();
                }
                break;
            default:
                if(!isStageCleared)
                {
                    isCrashed = true;
                    Debug.Log("Crashed!");
                    StartCrashSequence();

                }
                break;
        }
    }
    
    void StartCrashSequence() 
    {
        movement.enabled = false;
        movement.rocketSound.Stop(); 
        if(!movement.gameOverSound.isPlaying) 
        {
            movement.gameOverSound.Play();
        }
        
        Invoke("ReloadLevel", crashDelay);
    }

    void StartStageClearSequence() {
        movement.enabled = false;
        movement.rocketSound.Stop();
        if(!movement.stageClearSound.isPlaying)
        {
            movement.stageClearSound.Play();
        }
        Invoke("LoadNextLevel", stageClearDelay);
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
