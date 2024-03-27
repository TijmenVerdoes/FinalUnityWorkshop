using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("Player")] [SerializeField] 
    private GameObject player;
    
    [SerializeField] 
    private float timerDuration;
    
    [FormerlySerializedAs("PlayerDistance")] [SerializeField] 
    private float playerDistance;
    
    [SerializeField]
    private int lives;
    
    public GameObject prefab;
    private int amountHit;
    private Rigidbody body;
    private GameObject inversionInstance;
    private readonly float movementSpeed = 0.1f;
    private float previousTime;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveEnemyToPlayer();
    }
    
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

    private void moveEnemyToPlayer()
    {
        var PlayerInfo = player.transform.position;
        var position = transform.position;
        var distance = Vector3.Distance(PlayerInfo, position);
        if (distance > playerDistance)
        {
            position.x += (PlayerInfo.x - position.x) * Time.fixedDeltaTime * movementSpeed;
            position.z += (PlayerInfo.z - position.z) * Time.fixedDeltaTime * movementSpeed;
            transform.position = position;
        }
        else
        {
            var currentTime = Time.time;
            if (currentTime > previousTime + timerDuration)
            {
                previousTime = currentTime;
                inversionInstance = Instantiate(prefab, transform.position, transform.rotation);
            }
        }
    }
}