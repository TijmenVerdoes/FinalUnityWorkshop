using UnityEngine;

namespace Script
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] 
        private GameObject player;
    
        [SerializeField] 
        private float timerDuration;
    
        [SerializeField] 
        private float playerDistance;
    
        [SerializeField]
        private int lives;
    
        public GameObject prefab;
        private int _amountHit;
        private Rigidbody _body;
        private GameObject _inversionInstance;
        private const float MovementSpeed = 0.1f;
        private float _previousTime;
        public AudioSource shootSound;

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            MoveEnemyToPlayer();
        }
    
        public void GotHit()
        {
            _amountHit++;
            if (_amountHit > lives)
            {
                if (_inversionInstance != null)
                {
                    Destroy(_inversionInstance);
                }

                Destroy(gameObject, 0.1f);
            
            }
        }

        private void MoveEnemyToPlayer()
        {
            var playerInfo = player.transform.position;
            var position = transform.position;
            var distance = Vector3.Distance(playerInfo, position);
        
            if (distance > playerDistance)
            {
                position.x += (playerInfo.x - position.x) * Time.fixedDeltaTime * MovementSpeed;
                position.z += (playerInfo.z - position.z) * Time.fixedDeltaTime * MovementSpeed;
                transform.position = position;
            }
        
            else
            {
                var currentTime = Time.time;
                if (currentTime > _previousTime + timerDuration)
                {
                    _previousTime = currentTime;
                    _inversionInstance = Instantiate(prefab, transform.position, transform.rotation);
                    shootSound.Play(0);
                }
            }
        }
    }
}