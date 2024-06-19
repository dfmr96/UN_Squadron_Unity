using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {

        [field: SerializeField] public float Health { get; private set; }

        [field: SerializeField] public float Speed { get; } = 15;
        [field: SerializeField] public float InvulnerabilityTime { get; private set; }


        [field: SerializeField] public int RecoveryTime { get; private set;}

        [SerializeField] private SideScrollController sideScroll;

        [field: SerializeField] public GameObject DamagedFlames { get; private set;}

        [SerializeField] private Sprite[] aircraftSprites;
        [field: SerializeField] public Animator Anim { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }

        [field: SerializeField] public Vulkan FrontVulkan { get; private set; }
        private PlayerStateMachine _playerStateMachine;

        //Privados
        private float _horizontal;
        private SpriteRenderer _aircraftRenderer;
        private float _vertical;
        private BoxCollider2D _cameraCol;
        private BoxCollider2D _playerCol;
        [field: SerializeField] public float MaxHealth { get;  private set;}


        private void OnEnable()
        {
            EventBus.instance.OnBossDestroyed += OnBossDestroyed;
        }

        private void OnBossDestroyed()
        {
            AudioManager.instance.playerDamaged.Stop();
            enabled = false;
        }

        private void OnDisable()
        {
            EventBus.instance.OnBossDestroyed -= OnBossDestroyed;
        }

        private void Start()
        {
            FrontVulkan.InitVulkan();
            _playerStateMachine = new PlayerStateMachine(this);
            GetReferences();
            Health = MaxHealth;
            EventBus.instance.PlayerSpawned(this);
        }

        private void GetReferences()
        {
            _cameraCol = sideScroll.Bounds;
            transform.parent = sideScroll.transform;
            _playerCol = GetComponent<BoxCollider2D>();
            _aircraftRenderer = GetComponent<SpriteRenderer>();
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
        }

        private void Update()
        {
            FrontVulkan.Update();
            _playerStateMachine.Update();
            Movement();
            if (Input.GetKey(KeyCode.Space)) FrontVulkan.TryFire();


            if (Input.GetKey(KeyCode.Alpha0))
            {
                LoadingManager.Instance.LoadNewScene("TEST_Level1");
            }
        }

        private void Movement()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(_horizontal, _vertical).normalized;
            transform.Translate(dir * (Speed * Time.deltaTime));
            Anim.SetInteger("Vertical", (int)_vertical);

            CheckBounds();
        }

        private void CheckBounds()
        {
            //Chequeo de colisiones, si el jugador intenta atravesar la pantalla no podra
            if (_playerCol.bounds.min.x < _cameraCol.bounds.min.x && _horizontal == -1)
            {
                _horizontal = 0;
            }

            if (_playerCol.bounds.max.x > _cameraCol.bounds.max.x && _horizontal == 1)
            {
                _horizontal = 0;
            }

            if (_playerCol.bounds.min.y < _cameraCol.bounds.min.y && _vertical == -1)
            {
                _vertical = 0;
            }

            if (_playerCol.bounds.max.y > _cameraCol.bounds.max.y && _vertical == 1)
            {
                _vertical = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IPickupable pickupable))
            {
                pickupable.PickUp(this);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("Obstacles"))
            {
                Debug.Log("Impacto contra bala");
                if (collision.gameObject.CompareTag("Obstacles"))
                {
                    TakeDamage(2);
                }
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            EventBus.instance.PlayerDamaged(damage);

            if (_playerStateMachine.CurrentState == _playerStateMachine.HealthyState)
            {
                _playerStateMachine.ChangeStateTo(_playerStateMachine.DangerState);
                return;
            }

            if (_playerStateMachine.CurrentState == _playerStateMachine.DangerState 
                || _playerStateMachine.CurrentState == _playerStateMachine.CriticalState)
            {
                _playerStateMachine.ChangeStateTo(_playerStateMachine.DestroyedState);
                return;
            }
            
            if (Health <= 0)
            {
                _playerStateMachine.ChangeStateTo(_playerStateMachine.CriticalState);
                AudioManager.instance.playerUnableToRecover.Play();
            }
        }
    }
}
