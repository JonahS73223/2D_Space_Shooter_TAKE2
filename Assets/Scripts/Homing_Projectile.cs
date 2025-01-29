using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Projectile : MonoBehaviour
{
    private GameObject _targetEnemy;
    private float _speed = 8f;
    private float _rotatingSpeed = 200;

    Rigidbody2D _rb;
    void Start()
    {

        _targetEnemy = GameObject.FindGameObjectWithTag("Enemy");

        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.x > 8 && transform.position.x < -8)
        {
            Destroy(this.gameObject);
        }

        if (_targetEnemy != null)
        {
            Vector2 point2Target = (Vector2)transform.position - (Vector2)_targetEnemy.transform.position;

            point2Target.Normalize();

            float value = Vector3.Cross(point2Target, transform.right).z;

            if (value > 0)
            {
                _rb.angularVelocity = _rotatingSpeed;
            }
            else if (value < 0)
            {
                _rb.angularVelocity = -_rotatingSpeed;
            }
            else
            {
                _rotatingSpeed = 0;
            }

            _rb.velocity = transform.right * _speed;

            return;
        }
        
        
        
    }

    
    

    
}
