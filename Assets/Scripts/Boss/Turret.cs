using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform _targetPlayer;
    private float _fireRate = 4.0f;
    private float _canFire = -1;

   
    [SerializeField]
    private GameObject _canonShotPrefab;
    void Start()
    {
        _targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

       

    }

    // Update is called once per frame
    void Update()
    {
        if (_targetPlayer != null)
        {
            
            RotateTowardsTarget();
        }

        CanonShot();

        
    }

    private void RotateTowardsTarget()
    {


        var offset = 90f;
        Vector2 direction = _targetPlayer.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

    }


    private void CanonShot()
    {

        if (transform.position.y <= 4f)
        {
            if (Time.time > _canFire)
            {
                _fireRate = 3.5f;
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_canonShotPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            }
        }

        
    }

    
}
