using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRam : MonoBehaviour
{
    private Transform _targetPlayer;
    private float _speed = 2f;
    private float _dodgeRate = 1.0f;
    

    private bool _isLaserDodgeEnabeled = true;


    [SerializeField]
    private float _laserCastRadius = .5f;
    [SerializeField]
    private float _laserCastDistance = 5.0f;

    void Start()
    {
        
        
            _targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLaserDodgeEnabeled == true)
        {
            AvoidLaser();
        }
        

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

    void AvoidLaser()
    {

        RaycastHit2D Laserhit = Physics2D.CircleCast(transform.position, _laserCastRadius, Vector2.down, _laserCastDistance, LayerMask.GetMask("Laser"));

        if (Laserhit.collider != null)
        {
            if (Laserhit.collider.CompareTag("Laser"))
            {
                transform.position = new Vector3(transform.position.x - _dodgeRate, transform.position.y, transform.position.z);
                _dodgeRate -= .3f;
                _isLaserDodgeEnabeled = false;
                StartCoroutine(LaserDodgeCooldown());
                if (_dodgeRate <= 0f)
                {
                    _dodgeRate = .05f;
                }
            }
        }
    }

    IEnumerator LaserDodgeCooldown()
    {
        if (_isLaserDodgeEnabeled == false)
        {
            yield return new WaitForSeconds(3f);
            _isLaserDodgeEnabeled = true;
        }
    }
}
