using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc_Shot : MonoBehaviour
{

    [SerializeField]
    private List<Vector3> _travelPoints = new List<Vector3>();
    [SerializeField]
    private float _speed = 5;
    private Vector3 _currentTarget;
    private int _currentIndex = 0;


    void Start()
    {
        _currentTarget = new Vector3(-4.7f, 0.8f, 0f);

        _travelPoints.Add(new Vector3(-4.7f, 0.8f, 0f));
        _travelPoints.Add(new Vector3(-3f, -1f, 0f));
        _travelPoints.Add(new Vector3(1.5f, -2.3f, 0f));
        _travelPoints.Add(new Vector3(3.5f, -0.8f, 0f));
        _travelPoints.Add(new Vector3(6.7f, -0.8f, 0f));
        _travelPoints.Add(new Vector3(11.8f, -1f, 0f));
        _travelPoints.Add(new Vector3(10.2f, -3.3f, 0f));
        _travelPoints.Add(new Vector3(0.2f, -8.1f, 0f));
    }

    // Update is called once per frame
    void Update()
    {

        // Move toward the current target point
        if (_travelPoints.Count > 0 && _currentIndex < _travelPoints.Count)
        {
            MoveTowardsTarget();
        }

        if (transform.position.y < -8.0)
        {
            Destroy(this.gameObject);
        }

    }

    void MoveTowardsTarget()
    {
        
        if (Vector3.Distance(transform.position, _currentTarget) < 0.1f)
        {
           
            _currentIndex++;           
            if (_currentIndex >= _travelPoints.Count)
            {
                _currentIndex = 0;
            }
            
            _currentTarget = _travelPoints[_currentIndex];
        }

        
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            
            Destroy(this.gameObject, 2.8f);

        }


    }
}
