using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCanon : MonoBehaviour
{

    private float _fireRate = 4.0f;
    private float _canFire = -1;
    [SerializeField]
    private GameObject _blastShotPrefab;
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

                _fireRate = 10.5f;
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_blastShotPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            }
        }

        
    }
}
