using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField]
    private int _currentWave;

    [SerializeField]
    private int _maxWave;

    [SerializeField]
    private UI_Manager _uiManager;

    [SerializeField]
    private Spawn_Manager _spawnManager;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private int _spawnQty;

    [SerializeField]
    private int _spawnCount;

    [SerializeField]
    private int[] _maxEnemies;
    void Start()
    {
        _currentWave = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        WaveUpdate();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void WaveUpdate()
    {
        if (_currentWave == 0)
        {
            _spawnCount = _maxEnemies[0];
            _spawnQty = _maxEnemies[0];
            _uiManager.UpdateEnemyQty(_maxEnemies[0]);
        }

        if (_currentWave == 1)
        {
            _spawnManager.StartSpawning2();
            _spawnCount = _maxEnemies[1];
            _spawnQty = _maxEnemies[1];
            _uiManager.UpdateEnemyQty(_maxEnemies[1]);
            
        }

        if (_currentWave == _maxWave)
        {
            _gameManager.GameOver();
            _uiManager.GameOverSequence();
            _spawnManager.StopSpawning();
            
        }
    }

    public void QtyUpdate()
    {
        _spawnQty--;
        if (_spawnQty == 0)
        {
            _spawnManager.StopSpawning();
           
        }

        

        //Debug.LogError("asteriod destroyed");
    }

    public void CountUpdate()
    {
        _spawnCount--;
        if (_spawnCount == 0)
        {
            
            _currentWave++;
            WaveUpdate();
        }
        
        _uiManager.UpdateEnemyQty(_spawnCount);
    }
}
