using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rammer : MonoBehaviour
{
    private Player _player;
    
    private EnemyWaveManager _enemywaveManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _tlaserPrefab;

    private float _shrinkDuration = 1.4f;
    private Vector3 _targetScale = new Vector3(0, 0, 0);
    private Vector3 _startScale;

    private bool _enemyDeath = false;
    private bool _isbelowPlayer = false;

    private float _speed = 4.0f;
    private float _fireRate = 4.0f;
    private float _canFire = -1;


    void Start()
    {
        _enemywaveManager = GameObject.Find("EnemyWaveManager").GetComponent<EnemyWaveManager>();

        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        _startScale = transform.localScale;


    }

    // Update is called once per frame
    void Update()
    {
        Calculatemovement();
        StartCoroutine(ShrinkDown());
        
    }


    private void Calculatemovement()
    {


        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        if (transform.position.y <= -3f)
        {
            _isbelowPlayer = true;
            _speed = 5f;
            EnemyShoot();
        }

        if (transform.position.y <= -6f)
        {
            transform.position = new Vector3(Random.Range(-8.3f, 8.3f), 10, 0);
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

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0f;
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
           
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _speed = 0f;
            _audioSource.Play();
            _enemywaveManager.CountUpdate();
            _enemyDeath = true;
            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.4f);

        }

        if (other.tag == "Shieldshot")
        {

            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }
           
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _speed = 0f;
            _audioSource.Play();
            _enemywaveManager.CountUpdate();
            _enemyDeath = true;
            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.4f);

        }

        if (other.tag == "H.Missle")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0.2f;
            _audioSource.Play();
            _enemywaveManager.CountUpdate();

            Destroy(GetComponent<Collider2D>());
            _enemyDeath = true;
            Destroy(this.gameObject, 2.4f);

        }
    }


    IEnumerator ShrinkDown()
    {
        float elapsedTime = 0f;
        if (_enemyDeath == true)
        {
            while (elapsedTime < _shrinkDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(_startScale, _targetScale, elapsedTime / _shrinkDuration);
                yield return null;
            }
        }


    }

    private void EnemyShoot()
    {
        if (_isbelowPlayer == true)
        {
            if (Time.time > _canFire)
            {
                _fireRate = 0.8f;
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_tlaserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].AssignEnemyLaser();
                }
            }

        }
    }
}
