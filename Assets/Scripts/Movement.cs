using UnityEngine;

public class Movement : MonoBehaviour
{
    //How to layer variables
    //Parameters
    //Cache
    //State
   [SerializeField] float thrust = 100f;
   [SerializeField] float rotation = 100f;
   [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
   Rigidbody myRidgidBody;
    // Start is called before the first frame update
    AudioSource myAudioSource;


    void Start()
    {
       myRidgidBody = GetComponent<Rigidbody>();
       myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        
        if(Input.GetKey(KeyCode.Space))
        {
            
            myRidgidBody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if(!myAudioSource.isPlaying)
            {
            myAudioSource.PlayOneShot(mainEngine);
            }
            if(!mainBooster.isPlaying)
            {
               mainBooster.Play(); 
            }

        }
        else 
        {
            myAudioSource.Stop();
            mainBooster.Stop();
            
        }

    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotation);
            if(!leftBooster.isPlaying)
            {
               leftBooster.Play(); 
            }
            


        }
        else if(Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotation);
            if(!rightBooster.isPlaying)
            {
               rightBooster.Play(); 
            }


        } 
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

     void ApplyRotation(float rotationThisFrame)
    {
        myRidgidBody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRidgidBody.freezeRotation = false; // unfreezing rotation so physics system can take over
    }
}
