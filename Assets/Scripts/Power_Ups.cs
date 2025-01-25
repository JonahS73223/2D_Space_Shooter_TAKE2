using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Ups : MonoBehaviour
{
    private float _speed = 2.5f;
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _audioClip;
    private Player _player;
  
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

        PowerupAbsorb();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedShotActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.AmmoRecharge();
                        break;
                    case 4:
                        player.HealthRegen();
                        break;
                    case 5:
                        player.ShieldShotActive();
                        break;
                    case 6:
                        player.AmmoStealActive();
                        break;
                }       
            }

            Destroy(this.gameObject);
        }
       
        
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.Lerp(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

    private void PowerupAbsorb()
    {
        if (Input.GetKey(KeyCode.C))
        {
            MoveTowardsPlayer();
        }
    }
}
