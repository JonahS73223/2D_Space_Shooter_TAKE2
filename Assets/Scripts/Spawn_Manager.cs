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
    private GameObject _lightingEnemyPrefab;
    [SerializeField]
    private GameObject _asteriodPrefab;
    [SerializeField]
    private GameObject _rammerPrefab;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject[] _powerUpSP;

    [SerializeField]
    private bool _stopSpawningPowerups = false;
    
    public int _enemymovementID;
    private EnemyWaveManager _enemywaveManager;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        _enemywaveManager = GameObject.Find("EnemyWaveManager").GetComponent<EnemyWaveManager>();
    }
    public void StartSpawning1()
    {
        _stopSpawning = false;

        StartCoroutine(PowSpawn());
        StartCoroutine(ESpawnRoutine());
        StartCoroutine(ASpawnRoutine());
        StartCoroutine(SpecialtySpawn());
        StartCoroutine(LEnemySpawn());
        StartCoroutine(RamSpawn());

    }

    public void StartSpawning2()
    {
        _stopSpawning = false;
        
        StartCoroutine(PowSpawn());
        StartCoroutine(SpecialtySpawn());
        StartCoroutine(ESpawnRoutine2());
        StartCoroutine(ASpawnRoutine());
        StartCoroutine(LEnemySpawn());
        StartCoroutine(RamSpawn());
    }

    public void StartBossBattle()
    {
        _stopSpawning = false;
        BossSpawn();
        StartCoroutine(PowSpawn());
        StartCoroutine(SpecialtySpawn());
    }
    

    void Update()
    {
        
    }
   
    IEnumerator ESpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            _enemywaveManager.QtyUpdate();
            Vector3 spawnPoint0 = new Vector3(Random.Range(-8f, 8f), 7, 0);
           GameObject newEnemy0 = Instantiate(_enemyPrefab, spawnPoint0, Quaternion.identity);
           newEnemy0.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    IEnumerator ESpawnRoutine2()
    {
        yield return new WaitForSeconds(5f);
        while (_stopSpawning == false)
        {
            
            _enemywaveManager.QtyUpdate();
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
            yield return new WaitForSeconds(8.0f);
        }
    }

    IEnumerator RamSpawn()
    {
        yield return new WaitForSeconds(4f);
        while (_stopSpawning == false)
        {
            _enemywaveManager.QtyUpdate();
            Vector3 spawnPoint0 = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy0 = Instantiate(_rammerPrefab, spawnPoint0, Quaternion.identity);
            newEnemy0.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(12f);
        }
    }


    IEnumerator ASpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 7, 0);
            _asteriodPrefab.transform.localScale = new Vector3(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), 0);
            GameObject newAsteroid = Instantiate(_asteriodPrefab, spawnPoint, Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform; 
            yield return new WaitForSeconds(12.0f);
        }
    }
    
    public void BossSpawn()
    {
        _enemywaveManager.QtyUpdate();
        Vector3 spawnPoint = new Vector3(0, 10, 0);
        GameObject Boss = Instantiate(_bossPrefab, spawnPoint, Quaternion.identity);
        Boss.transform.parent = _enemyContainer.transform;
    }

   public void StopSpawning()
    {
        _stopSpawning = true;
    }

    public void StopSpawningPowerups()
    {
        _stopSpawningPowerups = true;
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    IEnumerator PowSpawn()
    {
        while (_stopSpawningPowerups == false)
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
        while (_stopSpawningPowerups == false)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomSPPowerUp = Random.Range(0, 4);
            GameObject newPower = Instantiate(_powerUpSP[randomSPPowerUp], spawnPoint, Quaternion.identity);
            newPower.transform.parent = _powContainer.transform;
            yield return new WaitForSeconds(Random.Range(15f, 20f));
        }
    }

    IEnumerator LEnemySpawn()
    {
        yield return new WaitForSeconds(5f);
        while (_stopSpawning == false)
        {
            _enemywaveManager.QtyUpdate();
            Vector3 spawnpoint = new Vector3(Random.Range(-6f, 6f), 7, 0);
            GameObject newEnemy = Instantiate(_lightingEnemyPrefab, spawnpoint, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(10f);
        }
    }
        

}
