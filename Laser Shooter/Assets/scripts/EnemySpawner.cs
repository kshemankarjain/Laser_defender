using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int StartingWave = 0;
    [SerializeField] List<WaveConfig> waveConfig;
    [SerializeField] bool looping = false;
 

    // Start is called before the first frame update
    IEnumerator  Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        }
        while (looping);

    }
    private IEnumerator SpawnAllWaves()
    {
        for (int waveindex = StartingWave;waveindex<waveConfig.Count;waveindex++)
        {
            var currentwave = waveConfig[waveindex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentwave));

        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemycount = 0; enemycount < waveConfig.GetNumberOfEnemies(); enemycount++)
        {
          var NewEnemy =   Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            NewEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

   
}
