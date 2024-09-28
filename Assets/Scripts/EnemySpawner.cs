using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfig;
    [SerializeField] int startingWave;
    [SerializeField] bool looping = true; 
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
            yield return StartCoroutine(SpawnAllWaves());
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        startingWave = Random.Range(0, waveConfig.Count/2);
        for(int WaveIndex = startingWave; WaveIndex<waveConfig.Count; WaveIndex++) 
        {
            var currentWave = waveConfig[WaveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) 
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetnumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweeSpawns());
        }
    }
}
