using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRam : MonoBehaviour
{
    private Transform _targetPlayer;
    private float _speed = 2f;
    [SerializeField]
    private GameObject player;
    
    void Start()
    {
        
        
            _targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
           
            

        transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
        if (_targetPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPlayer.position, _speed * Time.deltaTime);
            RotateTowardsTarget();
        }
        
    }

    private void RotateTowardsTarget()
    {
        
        
            var offset = 90f;
            Vector2 direction = _targetPlayer.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        
    }
        
}
