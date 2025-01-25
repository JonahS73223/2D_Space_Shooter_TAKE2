using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting_Enemy : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private GameObject _lightningBeam;
    [SerializeField]
    private GameObject _shield;
    private EnemyWaveManager _enemywaveManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _explosionPrefab;

    private float _shrinkDuration = 1.4f;
    private Vector3 _targetScale = new Vector3(0, 0, 0);
    private Vector3 _startScale;

    private bool _lightningActive = false;

    private float _speed = 3.0f;
    private bool _enemyDeath = false;
    private bool movingDown = true;
    private bool movingLeft = true;
    private bool _shieldactive = true;
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
        if (movingDown)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y <= 2f)
            {
                transform.position = new Vector3(transform.position.x, 2f, 0);
                _lightningActive = true;
                StartCoroutine(LightningActivated());
                movingDown = false;
            }
        }
        else
        {
            if (movingLeft)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);

                if (transform.position.x > 7f)
                {
                    movingLeft = false;
                }
            }

            else
            {

                transform.Translate(-Vector3.right * _speed * Time.deltaTime);
                if (transform.position.x < -7f)
                {
                    movingLeft = true;
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      

        if (other.tag == "Laser" )
        {
            if (_shieldactive == true)
            {
                _shield.SetActive(false);
                _shieldactive = false;

            }
            else
            {
                Destroy(other.gameObject);
                if (_player != null)
                {
                    _player.AddScore(Random.Range(5, 10));
                }
                _lightningBeam.SetActive(false);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _speed = 0.2f;
                _audioSource.Play();
                _enemywaveManager.CountUpdate();
                _enemyDeath = true;
                Destroy(GetComponent<Collider2D>());

                Destroy(this.gameObject, 2.4f);
            }
           

        }

        if (other.tag == "Shieldshot" )
        {
            if (_shieldactive == true)
            {
                _shield.SetActive(false);
                _shieldactive = false;

            }
            else
            {

                if (_player != null)
                {
                    _player.AddScore(Random.Range(5, 10));
                }
                _lightningBeam.SetActive(false);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _speed = 0.2f;
                _audioSource.Play();
                _enemywaveManager.CountUpdate();
                _enemyDeath = true;
                Destroy(GetComponent<Collider2D>());

                Destroy(this.gameObject, 2.4f);

            }

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

    IEnumerator LightningActivated()
    {
        while (_lightningActive == true)
        {
            yield return new WaitForSeconds(2f);
            _lightningBeam.SetActive(true);
            yield return new WaitForSeconds(1f);
            _lightningBeam.SetActive(false);
            

        }
    }

   
}
