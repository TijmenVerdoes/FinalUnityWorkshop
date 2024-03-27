using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] float PlayerDistance;
    private float movementSpeed = 0.1f;
    private int amountHit = 0;
    [SerializeField] private int lives;
    public GameObject prefab;
    private GameObject inversionInstance;
    private Rigidbody body;
    [SerializeField] private float timerDuration; 
    

    private float previousTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    public void GotHit()
    {
        amountHit++;
        if (amountHit > lives)
        {
            if (inversionInstance != null)
            {
                Destroy(inversionInstance);
            }
            
            Destroy(gameObject, 0.1f);
        }
    }
    void moveEnemyToPlayer()
    {
        Vector3 PlayerInfo = Player.transform.position;
        Vector3 position = transform.position;
        float distance = Vector3.Distance(PlayerInfo, position);
        if (distance > PlayerDistance)
        {
            position.x = position.x + ((PlayerInfo.x - position.x) * Time.fixedDeltaTime * movementSpeed);
            position.z = position.z + ((PlayerInfo.z - position.z) * Time.fixedDeltaTime* movementSpeed);
            transform.position = position;
            
        }
        else
        {
            float currentTime = Time.time;
            if (currentTime > (previousTime + timerDuration))
            {
                previousTime = currentTime;
                inversionInstance = Instantiate(prefab, transform.position ,transform.rotation);
            }
            
            
        }
    }
    
    private void FixedUpdate()
    {

            moveEnemyToPlayer();
        
        
    }
    void Update()
    {
        
    }
}
