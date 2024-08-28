using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("collide with Friendly object");
                break;
            case "Finish":
                Debug.Log("Collide with Finish object");
                break;
            case "Fuel":
                Debug.Log("Collide with Fuel object");
                break;
            default:
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
