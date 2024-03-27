using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeMovement : MonoBehaviour
{

    [SerializeField] 
    private GameObject player;
    
    [SerializeField] 
    private float cubeForce;
    
    [SerializeField] 
    private float cubeSpeed;
    
    private Rigidbody _body;
    private float _playerX;
    private float _playerY;
    private const float CubeLifetime = 1.0f;
    public static int BulletCount = 0;
    private float _beginTime = 0.0f;
    private bool _bugFix = true;

    private void Start()
    {
        var localPosition = transform.InverseTransformPoint(transform.position);
        var globalPosition = transform.TransformPoint(localPosition);
        var playerInfo = player.transform.position;
        
        _body = GetComponent<Rigidbody>();
        
        BulletCount++;

        _beginTime = Time.time + CubeLifetime;
        localPosition.x += 2;
        
        _body.AddRelativeForce (new Vector3(1, 1, 1) * cubeForce);
        transform.position = globalPosition;
        
        _playerX = playerInfo.x;
        _playerY = playerInfo.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            if (_bugFix)
            {
                _bugFix = false;
                BulletCount--;
                Destroy(gameObject, 0.1f);
            }
        }
    }
    
    private void FixedUpdate()
    {
        var position = transform.position;
        position.x += ((_playerX - position.x) * Time.fixedDeltaTime * cubeSpeed);
        position.z += ((_playerY - position.z) * Time.fixedDeltaTime* cubeSpeed);
        transform.position = position;
        
        var deltaRotation = Quaternion.Euler(new Vector3(2,2,2) * (Time.fixedDeltaTime * 100.0f));
        _body.MoveRotation(_body.rotation * deltaRotation);
        
        if (Time.time > _beginTime)
        {
            if (_bugFix)
            {
                _bugFix = false;
                BulletCount--;
                Destroy(gameObject);
            }
        }
    }
}