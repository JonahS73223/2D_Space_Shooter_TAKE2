using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private UI_Manager _uiManager;

    [SerializeField]
    private Spawn_Manager _spawnManager;

    

    [SerializeField]
    private float _health = 30f;
    private float _speed = 0.8f;
    private float _shrinkDuration = 1.4f;
    private Vector3 _targetScale = new Vector3(0, 0, 0);
    private Vector3 _startScale;

    private bool _enemyDeath = false;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _startScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= 4f)
        {
            transform.position = new Vector3(0, 4f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }


            Damage();
           
            
            
            

        }

        if (other.tag == "Shieldshot")
        {
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }

            Damage();




            
           

        }

        if (other.tag == "H.Missle")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }


            Damage();




            
            

        }
    }

    private void Damage()
    {
        _health--;
        _audioSource.Play();
        Vector3 newPos = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
        if (_health <= 0)
        {
            _enemyDeath = true;
            Instantiate(_explosionPrefab, newPos, Quaternion.identity);
            StartCoroutine(ShrinkDown());
            Destroy(this.gameObject, 3.0f);
            _gameManager.GameOver();
            _uiManager.GameOverSequence();
            _spawnManager.StopSpawningPowerups();
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
}
