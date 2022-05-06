using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelChangeDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    Movement MovementChange;
    AudioSource myAudioSource;
    bool isTransitioning = false;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning){return;}

        switch (collision.gameObject.tag) 
        {
            case "Friendly" : 
            Debug.Log("You hit something friendly");
                break;
            case "Finish" : 
                StartSuccessSequence();
                break;
            default :
            StartCrashSequence();
            break;
        }
    }
        void StartCrashSequence()
        {
            //add sound effect on crash
            //add partical effect on crash
            isTransitioning = true;
            myAudioSource.Stop();
            MovementChange = GetComponent<Movement>();
            MovementChange.enabled = false;
            myAudioSource.PlayOneShot(crash);
            crashParticles.Play();
            

            Invoke("ReloadLevel",levelChangeDelay);
            
        }
        void StartSuccessSequence()
        {
                        //add sound effect on success
            //add partical effect on success
            isTransitioning = true;
            myAudioSource.Stop();
            MovementChange = GetComponent<Movement>();
            MovementChange.enabled = false;
            myAudioSource.PlayOneShot(success);
            successParticles.Play();

            Invoke("NextLevel", levelChangeDelay);
        }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex );
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = currentSceneIndex+ 1;
        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0 ;
        }
        SceneManager.LoadScene(NextSceneIndex );
    }
}
