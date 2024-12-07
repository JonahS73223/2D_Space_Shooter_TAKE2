using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.0f;
    private Player _player;
    private Animator _anim;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 4.0f;
    private float _canFire = -1;
    private bool _enemyDeath = false;
    
    // Start is called before the first frame update
    void Start()
    {
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
        CalculateMovement();
        EnemyShoot();
    }

    void EnemyShoot()
    {

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(5f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
        if (_enemyDeath == true)
        {
            _fireRate = 0;
        }
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(Random.Range(-8.3f, 8.3f), 8, 0);
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
            Destroy(this.gameObject, 2.8f);
            
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            _enemyDeath = true;
            Destroy(this.gameObject, 2.4f);
            
        }

        if(other.tag == "Shieldshot")
        {
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            _enemyDeath = true;
            Destroy(this.gameObject, 2.4f);
        }
    }
}
