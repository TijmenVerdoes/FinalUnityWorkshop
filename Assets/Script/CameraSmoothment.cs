using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothment : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (!target) 
        {
            return;
        }

        var targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        targetPosition.y = transform.position.y; 

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
