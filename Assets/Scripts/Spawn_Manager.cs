using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _powContainer;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _asteriodPrefab;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject[] _powerUpSP;
   
    
    public int _enemymovementID;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartSpawning()
    {
        StartCoroutine(PowSpawn());
        StartCoroutine(ESpawnRoutine());
        StartCoroutine(ASpawnRoutine());
        StartCoroutine(SpecialtySpawn());
    }
    
    void Update()
    {
        
    }
   
    IEnumerator ESpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            _enemymovementID = Random.Range(0, 3);
            switch (_enemymovementID)
            {
                case 0: //Down
                    Vector3 spawnPoint0 = new Vector3(Random.Range(-8f, 8f), 7, 0);
                    GameObject newEnemy0 = Instantiate(_enemyPrefab, spawnPoint0, Quaternion.identity);
                    newEnemy0.transform.parent = _enemyContainer.transform;
                    break;
                case 1: //RightDown
                    Vector3 spawnPoint1 = new Vector3(Random.Range(-8f, 0f), 7, 0);
                    GameObject newEnemy1 = Instantiate(_enemyPrefab, spawnPoint1, Quaternion.identity);
                    newEnemy1.transform.parent = _enemyContainer.transform;
                    break;
                case 2: // LeftDown
                    Vector3 spawnPoint2 = new Vector3(Random.Range(8f, 0f), 7, 0);
                    GameObject newEnemy2 = Instantiate(_enemyPrefab, spawnPoint2, Quaternion.identity);
                    newEnemy2.transform.parent = _enemyContainer.transform;
                    break;
            }
            yield return new WaitForSeconds(3.0f);
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

    IEnumerator PowSpawn()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 4);
            GameObject newPower = Instantiate(_powerUps[randomPowerUp], spawnPoint, Quaternion.identity);
            newPower.transform.parent = _powContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    IEnumerator SpecialtySpawn()
    {
        yield return new WaitForSeconds(7f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomSPPowerUp = Random.Range(0, 2);
            GameObject newPower = Instantiate(_powerUpSP[randomSPPowerUp], spawnPoint, Quaternion.identity);
            newPower.transform.parent = _powContainer.transform;
            yield return new WaitForSeconds(Random.Range(15f, 20f));
        }
    }
}
