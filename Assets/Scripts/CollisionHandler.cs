using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    float levelLoadDelay = 1.5f;

    [SerializeField]
    AudioClip crash;

    [SerializeField]
    AudioClip success;

    [SerializeField]
    ParticleSystem crashParticles;

    [SerializeField]
    ParticleSystem successParticles;

    AudioSource audioSource;
    BoxCollider boxCollider;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        RespondToDebugKeys();
        // DisableCollision();          MY SOLUTION FOR V.53
        // CheatLoadNextLevel();        MY SOLUTION FOR V.53
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle value to true/false
            switch (collisionDisabled)
            {
                case true:
                    Debug.Log("Collision disabled");
                    break;
                case false:
                    Debug.Log("Collision enabled");
                    break;;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("collide with Friendly object");
                break;
            case "Finish":
                Debug.Log("Collide with Finish object");
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        //todo add SFX upon success
        //todo add particle effect upon success
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        //todo add SFX upon crash
        //todo add particle effects upon crash
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
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
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // void DisableCollision()      MY SOLUTION FOR V.53
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         collisionDisabled = !collisionDisabled; //toggle collisionDisable to true/false;
    //         Debug.Log("Key C pressed. Collision Disabled");
    //         boxCollider.enabled = false;
    //     }
    // }

    // void CheatLoadNextLevel()    MY SOLUTION FOR V.53
    // {
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         Debug.Log("Key L pressed. Load Next Level");
    //         Invoke("LoadNextLevel", levelLoadDelay);
    //     }
    // }
}
