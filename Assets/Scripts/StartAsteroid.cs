using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAsteroid : MonoBehaviour
{
    private float _rotateSpeed = 15.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Spawn_Manager _spawnManager;
    [SerializeField]
    private AudioSource _audioSource;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _audioSource.Play();
            _spawnManager.StartSpawning1();
            Destroy(this.gameObject, 0.55f);
        }
    }
}
