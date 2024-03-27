using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    

    private float cubeLifetime = 3.0f;
    private float beginTime = 0.0f;
    [SerializeField] float cubeForce;
    private bool bugFix = true;
    private Rigidbody body;
    
    private void Start()
    {
        beginTime = Time.time + cubeLifetime;
        var localPosition = this.transform.InverseTransformPoint(transform.position);
        localPosition.x = localPosition.x + 2;
        body = GetComponent<Rigidbody>();
        body.AddRelativeForce (new Vector3(1, 0.5f, 0) * cubeForce);
        var globalposition = transform.TransformPoint(localPosition);
        transform.position = globalposition;
        
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     //Debug.Log("Collision");
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         // collision.gameObject.GotHit();
    //         if (bugFix)
    //         {
    //             bugFix = false;
    //             howmanyBullets--;
    //             Destroy(gameObject, 0.1f);
    //         }
    //     }
    // }


    private void FixedUpdate()
    {
        var deltaRotation = Quaternion.Euler(new Vector3(2,2,2)* Time.fixedDeltaTime * 100.0f );
        body.MoveRotation(body.rotation * deltaRotation);
        if (Time.time > beginTime)
        {
            if (bugFix)
            {
                bugFix = false;
                Destroy(gameObject);
            }
        }
    }
}
