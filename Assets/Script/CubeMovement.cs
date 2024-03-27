using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{

    [SerializeField] 
    private GameObject Player;
    
    [SerializeField] 
    float cubeForce;
    
    [SerializeField] 
    float cubeSpeed;
    
    private Rigidbody body;
    private float PlayerX;
    private float PlayerY;
    private float cubeLifetime = 3.0f;
    public static int howmanyBullets = 0;
    private float beginTime = 0.0f;
    private bool bugFix = true;

    
    
    void Start()
    {
        howmanyBullets++;
        body = GetComponent<Rigidbody>();
        beginTime = Time.time + cubeLifetime;
        Vector3 localPosition = this.transform.InverseTransformPoint(transform.position);
        localPosition.x = localPosition.x + 2;
        body.AddRelativeForce (new Vector3(1, 1, 1) * cubeForce);
        Vector3 globalposition = transform.TransformPoint(localPosition);
        transform.position = globalposition;
        Vector3 PlayerInfo = Player.transform.position;
        PlayerX = PlayerInfo.x;
        PlayerY = PlayerInfo.y;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            if (bugFix)
            {
                bugFix = false;
                howmanyBullets--;
                Destroy(gameObject, 0.1f);
            }
        }
    }


    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.x = position.x + ((PlayerX - position.x) * Time.fixedDeltaTime * cubeSpeed);
        position.z = position.z + ((PlayerY - position.z) * Time.fixedDeltaTime* cubeSpeed);
        transform.position = position;
        
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(2,2,2)* Time.fixedDeltaTime * 100.0f );
        body.MoveRotation(body.rotation * deltaRotation);
        if (Time.time > beginTime)
        {
            if (bugFix)
            {
                bugFix = false;
                howmanyBullets--;
                Destroy(gameObject);
            }
        }
    }
}
