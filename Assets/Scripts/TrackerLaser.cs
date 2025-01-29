using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerLaser : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D rb;
    private float _speed = 7.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            Vector2 direction = _player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * _speed;

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 8 && transform.position.x < -8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }


    }
}

