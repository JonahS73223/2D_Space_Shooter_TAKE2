﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 7.0f;
    private bool _isEnemyLaser = false;
    [SerializeField]
    private AudioClip _audioClip;
  

   
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUP();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUP()
    {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }

        if (other.tag == "Powerup" && _isEnemyLaser == true)
        {
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }



    }


    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }
}
