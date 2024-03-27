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
    private float playerX;
    private float playerY;
    private float cubeLifetime = 3.0f;
    public static int howmanyBullets = 0;
    private float beginTime = 0.0f;
    private bool bugFix = true;

    
    
    private void Start()
    {
        howmanyBullets++;
        body = GetComponent<Rigidbody>();
        beginTime = Time.time + cubeLifetime;
        var localPosition = this.transform.InverseTransformPoint(transform.position);
        localPosition.x = localPosition.x + 2;
        body.AddRelativeForce (new Vector3(1, 1, 1) * cubeForce);
        var globalposition = transform.TransformPoint(localPosition);
        transform.position = globalposition;
        var PlayerInfo = Player.transform.position;
        playerX = PlayerInfo.x;
        playerY = PlayerInfo.y;
    }

    private void OnCollisionEnter(Collision collision)
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
        var position = transform.position;
        position.x += ((playerX - position.x) * Time.fixedDeltaTime * cubeSpeed);
        position.z += ((playerY - position.z) * Time.fixedDeltaTime* cubeSpeed);
        transform.position = position;
        
        var deltaRotation = Quaternion.Euler(new Vector3(2,2,2)* Time.fixedDeltaTime * 100.0f );
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