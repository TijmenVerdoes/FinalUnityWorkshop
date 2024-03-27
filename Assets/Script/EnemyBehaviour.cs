using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] 
    private GameObject Player;
    
    [SerializeField] 
    private float timerDuration;
    
    [SerializeField] 
    private float PlayerDistance;
    
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
        var PlayerInfo = Player.transform.position;
        var position = transform.position;
        var distance = Vector3.Distance(PlayerInfo, position);
        if (distance > PlayerDistance)
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