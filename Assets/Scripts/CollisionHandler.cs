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
                break;
            default:
                Debug.Log("Crashed!");
                ReloadLevel();
                break;
        }
    }

    private static void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
