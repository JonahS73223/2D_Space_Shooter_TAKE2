using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5.0f;
   
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trishotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private float _fireRate = 0.3f;
    private float _canfire = -1;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;

    private Spawn_Manager _spawnManager;

    private bool _isTripleshotActive = false;
    private bool _isSpeedShotActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _thusterSize;
   // private bool _thrusterOff = false;
    
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();

         if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }

         if (_uiManager == null)
        {
            Debug.LogError("UI_Manager is NULL");
        }
        transform.position = new Vector3(0, -6, 0);

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on player is Null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

  
    void Update()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canfire)
        {
            Shoot();
        }
        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x > 10.3f)
        {
            transform.position = new Vector3(-10.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.3f)
        {
            transform.position = new Vector3(10.3f, transform.position.y, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 10f;
            _uiManager.Deduct(0.1f);
            _thusterSize.transform.localScale = new Vector3(1.0f, 1.5f, 0);
        }
        else
        {
            _uiManager.Add(5);
            _speed = 5f;
            _thusterSize.transform.localScale = new Vector3(1.0f, 1.0f, 0);
        }
       
    }

    void Shoot()
    {
       
        _canfire = Time.time + _fireRate;
        if (_isTripleshotActive == true)
        {
            Instantiate(_trishotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        }

        if (_isSpeedShotActive == true)
        {
            _canfire = 1;
           
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;


        if (_lives == 2)
        {
            _leftEngine.gameObject.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.gameObject.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleshotActive = true;
        StartCoroutine(TripleShotDownRoutine());
    }

    IEnumerator TripleShotDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleshotActive = false;
    }

    public void SpeedShotActive()
    {
        _isSpeedShotActive = true;
        StartCoroutine(SpeedShotDownRoutine());
    }

    IEnumerator SpeedShotDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedShotActive = false;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void ThrusterDeActivate()
    {
       // _thrusterOff = true;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 5.0f;
            _thusterSize.transform.localScale = new Vector3(1.0f, 1.0f, 0);
        }
    }
}
