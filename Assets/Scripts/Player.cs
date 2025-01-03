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
    private GameObject _shieldshotPrefab;
  
    private float _fireRate = 0.3f;
    private float _canfire = -1;
    [SerializeField]
    private int _lives = 3;
    private bool _ismaxLives = true;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _ammo = 15;

    private UI_Manager _uiManager;
    private CameraShake _cameraShake;
    private Spawn_Manager _spawnManager;

    private bool _isTripleshotActive = false;
    private bool _isSpeedShotActive = false;
    private bool _isShieldActive = false;
    private bool _isShieldshotActive = false;

    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _lowAmmoSFX;
    [SerializeField]
    private AudioClip _noAmmoSFX;
    [SerializeField]
    private AudioClip _ammoRechargeSFX;

    [SerializeField]
    private GameObject _thusterSize;
    [SerializeField]
    private GameObject _maxShield;
    [SerializeField]
    private GameObject _midShield;
    [SerializeField]
    private GameObject _lowShield;
    [SerializeField]
    private int _shieldPow;

   
   // private bool  = false;
    
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _audioSource = GetComponent<AudioSource>();
        _ammo = 15;
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
            if (_ammo == 0)
            {
                _ammo = 0;
                AudioSource.PlayClipAtPoint(_noAmmoSFX, transform.position);
                return;
            }
            Shoot();
        }
        if (_lives > 3)
        {
            _lives = 3;
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
        _ammo--;
        _uiManager.DeductAMMO(_ammo); 
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
            _fireRate = 0.05f;
            
        }
        _audioSource.Play();

        if (_ammo == 3)
        {
            AudioSource.PlayClipAtPoint(_lowAmmoSFX, transform.position);

        }
        
        if (_isShieldshotActive == true)
        {
            
            Instantiate(_shieldshotPrefab, transform.position, Quaternion.identity);
        }
      
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
           // ShieldActive();
            _shieldPow--;

            switch (_shieldPow)
            {
                case 0:
                    _isShieldActive = false;
                    _lowShield.SetActive(false);
                    _midShield.SetActive(false);
                    _maxShield.SetActive(false);                  
                    return;
                case 1:
                    _lowShield.SetActive(true);
                    _midShield.SetActive(false);
                    _maxShield.SetActive(false);
                    
                    return;
                case 2:
                    _midShield.SetActive(true);
                    _lowShield.SetActive(false);
                    _maxShield.SetActive(false);
                    
                    return;
                    
                case 3:
                    _maxShield.SetActive(true);
                    _midShield.SetActive(false);
                    _lowShield.SetActive(false);
                    
                    return;

                    
            }
        }
        _lives--;
        _cameraShake.ShakePlayer();

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

    public void ShieldShotActive()
    {
        _isShieldshotActive = true;
        _ammo = 15;
        _uiManager.AddAMMO(15);
        AudioSource.PlayClipAtPoint(_ammoRechargeSFX, transform.position);
        _fireRate = 1f;
        StartCoroutine(ShieldShotDownRoutine());
    }

    IEnumerator ShieldShotDownRoutine()
    {
        yield return new WaitForSeconds(7f);
        _fireRate = 0.3f;
        _isShieldshotActive = false;
    }
    public void SpeedShotActive()
    {
        _isSpeedShotActive = true;
        
        _ammo = 15;
        _uiManager.AddAMMO(15);
        AudioSource.PlayClipAtPoint(_ammoRechargeSFX, transform.position);
        StartCoroutine(SpeedShotDownRoutine());
    }

    IEnumerator SpeedShotDownRoutine()
    {
        yield return new WaitForSeconds(3f);
        _isSpeedShotActive = false;
        
        _fireRate = 0.3f;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldPow = 3;
        _maxShield.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void ThrusterDeActivate()
    {
       

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 5.0f;
            _thusterSize.transform.localScale = new Vector3(1.0f, 1.0f, 0);
        }
    }

    public void AmmoRecharge()
    {
        
        AudioSource.PlayClipAtPoint(_ammoRechargeSFX, transform.position);
        _uiManager.AddAMMO(15);
        _ammo = 15;
        
    }

    public void HealthRegen()
    {
        _lives += 1;
        if (_ismaxLives == true)
        {
            _lives = 3;
            _uiManager.UpdateLives(_lives);

        }

        if (_lives == 3)
        {
            _leftEngine.gameObject.SetActive(false);
            _rightEngine.gameObject.SetActive(false);

            _ismaxLives = true;
        }
        else if (_lives == 2)
        {
            _leftEngine.gameObject.SetActive(true);
            _ismaxLives = false;
        }
        else if (_lives == 1)
        {
            _rightEngine.gameObject.SetActive(true);
            _ismaxLives = false;
        }

        _uiManager.UpdateLives(_lives);

    }
}
