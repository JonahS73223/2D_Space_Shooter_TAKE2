using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Canon : MonoBehaviour
{
    private float _fireRate = 4.0f;
    private float _canFire = -1;
    [SerializeField]
    private GameObject _discShotPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 4f)
        {
            if (Time.time > _canFire)
            {

                _fireRate = 15.5f;
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_discShotPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            }
        }
        
    }
}
