using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField] float thrust = 100f;
   [SerializeField] float rotation = 100f;
   Rigidbody myRidgidBody;
    // Start is called before the first frame update
    void Start()
    {
       myRidgidBody = GetComponent<Rigidbody>();
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
        }

    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotation);
        }
        else if(Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotation);
        } 
    }

     void ApplyRotation(float rotationThisFrame)
    {
        myRidgidBody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRidgidBody.freezeRotation = false; // unfreezing rotation so physics system can take over
    }
}
