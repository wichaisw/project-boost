using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float stageClearDelay = 3f;
    [SerializeField] float crashDelay = 1.5f;
    [SerializeField] public AudioClip gameOverSound;
    [SerializeField] public AudioClip stageClearSound;
    [SerializeField] ParticleSystem gameOverParticles;
    [SerializeField] ParticleSystem stageClearParticles;

    // CACHE
    Movement movement;
    AudioSource audioSource;

    // STATES
    bool isStageCleared = false;
    bool isAlive = true;
    bool isTransitioning = false;
    bool isCollisionDisable = false;

    void Start()
    {   
        isStageCleared = false;
        isAlive = true;
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    void Update() 
    {
        RespondToDebugKey();
    }

    void RespondToDebugKey() 
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            LoadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisable = !isCollisionDisable; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other) {

        if(isTransitioning || isCollisionDisable ) { return; }

        switch(other.gameObject.tag) {
            case "Friendly":
                break;
            case "Fuel":
                Debug.Log("collide Fuel");
                break;
            case "Finish":
                if(isAlive) 
                {   
                    isStageCleared = true;
                    StartStageClearSequence();
                }
                break;
            default:
                if(!isStageCleared)
                {
                    isAlive = false;
                    StartCrashSequence();

                }
                break;
        }
    }
    
    void StartCrashSequence() 
    {

        isTransitioning = true;
        gameOverParticles.Play();
        movement.enabled = false;
        audioSource.Stop(); 
        audioSource.PlayOneShot(gameOverSound);
        
        Invoke("ReloadLevel", crashDelay);
    }

    void StartStageClearSequence() {

        stageClearParticles.Play();
        isTransitioning = true;
        movement.enabled = false;
        audioSource.Stop();
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
