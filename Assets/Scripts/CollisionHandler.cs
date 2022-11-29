using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler: MonoBehaviour {

    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip sucessAudioClip;
    [SerializeField] AudioClip crashAudioClip;

    [SerializeField] ParticleSystem sucessParticels;
    [SerializeField] ParticleSystem crashParticels;

    AudioSource audioSource;
    BoxCollider bc;

    bool isTransitioning = false;
    bool collisonEnabled = true;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider>();
    }

    void Update() {
        DebugKeys();
    }

    void DebugKeys() {
        if (Input.GetKeyDown(KeyCode.L))
            LoadNextLevel ();

        if (Input.GetKeyDown(KeyCode.C)) {
            if (collisonEnabled)
                collisonEnabled = !collisonEnabled;
            else
                collisonEnabled = !collisonEnabled;
        }
    }

    void OnCollisionEnter (Collision collision) {

        if (isTransitioning || !collisonEnabled) return;

        switch (collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Success();
                break;
            default:
                Crash();
                break;
        }
    }

    void Crash () {
        isTransitioning = true;

        crashParticels.Play();

        audioSource.Stop();
        audioSource.PlayOneShot(crashAudioClip);

        GetComponent<Mover>().enabled = false;
        
        Invoke(nameof(ReloadLevel), delay);
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void Success() {
        isTransitioning = true;

        sucessParticels.Play();

        audioSource.Stop();
        audioSource.PlayOneShot(sucessAudioClip);

        GetComponent<Mover>().enabled = false;
        
        Invoke(nameof(LoadNextLevel), delay);
    }

    void LoadNextLevel() {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = 0;

        SceneManager.LoadScene(nextLevelIndex);
    }
}
