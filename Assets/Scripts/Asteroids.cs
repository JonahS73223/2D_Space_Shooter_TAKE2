using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    Player _player;
    [SerializeField]
    private AudioSource _audioSource;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(Random.Range(-8.3f, 8.3f), 8, 0);
            transform.localScale = new Vector3(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), 0);
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
            _audioSource.Play();
            _speed = 0.2f;
            Destroy(this.gameObject,0.75f);
        }

        if (other.tag == "Laser")
        {
            
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();
            _speed = 0.2f;

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 0.75f);
        }

        if (other.tag == "Shieldshot")
        {
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 10));
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();
            _speed = 0.2f;

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 0.75f);
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
            
            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 0.75f);

        }
    }
}
