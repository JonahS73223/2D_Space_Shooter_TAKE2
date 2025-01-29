using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Player _player;
    private Animator _anim;
    private Spawn_Manager _spawnManager;
    private EnemyWaveManager _enemywaveManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;



   
    private float _speed = 3.0f;
    private float _fireRate = 4.0f;
    private float _canFire = -1;
    private bool _enemyDeath = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemywaveManager = GameObject.Find("EnemyWaveManager").GetComponent<EnemyWaveManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {


       

        EnemyMovement();
        EnemyShoot();
    }

    void EnemyMovement()
    {
        if (_spawnManager._enemymovementID == 0) 
        {
            CalculateMovementDown();
            
        }

        if (_spawnManager._enemymovementID == 1)
        {
            CalculateMovementRightDown();
        }

        if (_spawnManager._enemymovementID == 2)
        {
            CalculateMovementLeftDown();
        }
    }

    void EnemyShoot()
    {
       
            if (_enemyDeath == true)
            {
                _fireRate = 0;
                _canFire = Time.time + _fireRate;
            }

            if (_spawnManager._enemymovementID == 1)
            {
                _fireRate = 1.0f;

            }
            else if (_spawnManager._enemymovementID == 2)
            {
                _fireRate = 1.0f;

            }

            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(2f, 3f);
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].AssignEnemyLaser();
                }
            }
        


       
       
    }
    void CalculateMovementDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(Random.Range(-8.3f, 8.3f), 8, 0);
        }
    }

    void CalculateMovementLeftDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(Random.Range(0f, 8.3f), 8, 0);
        }
    }

    void CalculateMovementRightDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(Random.Range(-8.3f, 0f), 8, 0);
        }
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            _audioSource.Play();
            _enemyDeath = true;
            _enemywaveManager.CountUpdate();
            Destroy(this.gameObject, 2.8f);

        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            _audioSource.Play();
            _enemywaveManager.CountUpdate();

            Destroy(GetComponent<Collider2D>());
            _enemyDeath = true;
            Destroy(this.gameObject, 2.4f);

        }

        if (other.tag == "Shieldshot")
        {
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            _audioSource.Play();
            _enemywaveManager.CountUpdate();

            Destroy(GetComponent<Collider2D>());
            _enemyDeath = true;
            Destroy(this.gameObject, 2.4f);

        }

        if (other.tag == "H.Missle")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            _audioSource.Play();
            _enemywaveManager.CountUpdate();

            Destroy(GetComponent<Collider2D>());
            _enemyDeath = true;
            Destroy(this.gameObject, 2.4f);

        }
    }

   
}
