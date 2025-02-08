using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastShot : MonoBehaviour
{
    private GameObject _player;
    private float _speed = 4f;
    private float _rotatingSpeed = 200;

    private float _shrinkDuration = 5f;
    private Vector3 _targetScale = new Vector3(0, 0, 0);
    private Vector3 _startScale;

    Rigidbody2D _rb;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _rb = GetComponent<Rigidbody2D>();

        _startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        Destroy(this.gameObject , 5f);
        StartCoroutine(ShrinkDown());

        if (_player != null)
        {
            Vector2 point2Target = (Vector2)transform.position - (Vector2)_player.transform.position;

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

    IEnumerator ShrinkDown()
    {
        float elapsedTime = 0f;
        
        
            while (elapsedTime < _shrinkDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(_startScale, _targetScale, elapsedTime / _shrinkDuration);
                yield return null;
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
