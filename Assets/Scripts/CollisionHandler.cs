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
    Rigidbody myRigidBody;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugKeys();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || collisionDisabled){return;}

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
    void DebugKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            NoCollision();
        }
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
        private void NoCollision()
    {
        collisionDisabled = !collisionDisabled;
        Debug.Log(collisionDisabled);

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


 


}
