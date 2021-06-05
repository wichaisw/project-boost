using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float stageClearDelay = 3f;
    [SerializeField] float crashDelay = 1.5f;
    [SerializeField] public AudioClip gameOverSound;
    [SerializeField] public AudioClip stageClearSound;

    // CACHE
    Movement movement;
    AudioSource audioSource;

    // STATES
    bool isStageCleared = false;
    bool isAlive = true;
    bool isTransitioning = false;

    void Start()
    {   
        isStageCleared = false;
        isAlive = true;
        audioSource = GetComponent<AudioSource>();
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
                if(isAlive) 
                {   
                    isStageCleared = true;
                    Debug.Log("collide Finish");
                    StartStageClearSequence();
                }
                break;
            default:
                if(!isStageCleared)
                {
                    isAlive = false;
                    Debug.Log("Crashed!");
                    StartCrashSequence();

                }
                break;
        }
    }
    
    void StartCrashSequence() 
    {
        if(isTransitioning) { return; }

        isTransitioning = true;
        movement.enabled = false;
        movement.audioSource.Stop(); 
        audioSource.PlayOneShot(gameOverSound);
        
        Invoke("ReloadLevel", crashDelay);
    }

    void StartStageClearSequence() {

        if(isTransitioning) { return; }

        movement.enabled = false;
        movement.audioSource.Stop();
        audioSource.PlayOneShot(stageClearSound);

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
