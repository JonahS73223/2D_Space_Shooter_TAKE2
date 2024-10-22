using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _asteriodPrefab;


    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(ESpawnRoutine());
        StartCoroutine(ASpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ESpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f) ,7 ,0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator ASpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 7, 0);
            _asteriodPrefab.transform.localScale = new Vector3(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), 0);
            GameObject newAsteroid = Instantiate(_asteriodPrefab, spawnPoint, Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform; 
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
