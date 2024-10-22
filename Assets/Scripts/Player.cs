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
    private float _fireRate = 0.3f;
    private float _canfire = -1;
    [SerializeField]
    private int _lives = 3;

    private Spawn_Manager _spawnManager;

    private bool _isTripleshotActive;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
         if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }
        transform.position = new Vector3(0, -6, 0);
    }

    // Update is called once per frame
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



    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
