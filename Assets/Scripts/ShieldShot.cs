using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldShot : MonoBehaviour
{
    private float _speed = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUP();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
        }
    }
}
