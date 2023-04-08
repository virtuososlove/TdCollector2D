using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] List<Transform> enemySpawnPositionList;
    [SerializeField] Transform nextWaveSpawnPosition;
    private Transform enemySpawnPosition;
    private float nextWaveSpawntimer;
    private float nextEnemySpawnTimer;
    private int waveCounter = 0;
    private int remaininEnemyCount;
    public event EventHandler onEventNumberChanged;
    private enum State
    {
        WaitingToSpawnNextWave,
        spawningWave,
    }
    private State state;

    void Start()
    {
        state = State.WaitingToSpawnNextWave;
        enemySpawnPosition = enemySpawnPositionList[UnityEngine.Random.Range(0, enemySpawnPositionList.Count)];
        nextWaveSpawnPosition.position = enemySpawnPosition.position;
    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                // assigning the necessary values ​​before the spawn wave
                nextWaveSpawntimer -= Time.deltaTime;
                if (nextWaveSpawntimer <= 0)
                {
                    SpawnWave();
                }
                break;
            
            case State.spawningWave:
                // Wave spawning
                if (remaininEnemyCount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer <= 0)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        Enemy.Create(enemySpawnPosition.position + UtilsClass.GetRandomDir() * 2f + new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f)));
                        remaininEnemyCount--;
                    }
                    if(remaininEnemyCount <= 0)
                    {
                        state = State.WaitingToSpawnNextWave;
                        enemySpawnPosition = enemySpawnPositionList[UnityEngine.Random.Range(0, enemySpawnPositionList.Count)];
                        nextWaveSpawnPosition.position = enemySpawnPosition.position;
                    }
                }
                break;
            
        }

    }
    private void SpawnWave()
    {
        // necessary values
        nextWaveSpawntimer = 10;
        waveCounter++;
        remaininEnemyCount = 5 + 3 * waveCounter;
        state = State.spawningWave;
        onEventNumberChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetWaveCounter()
    {
        return waveCounter;
    }
    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawntimer;
    }
}
