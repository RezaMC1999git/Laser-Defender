using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyWaveConfig")]
public class WaveConfig : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweeSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.2f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWaypoints() 
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints; 
    }
    public float GetTimeBetweeSpawns() { return timeBetweeSpawns; }
    public float GetspawnRandomFactor() { return spawnRandomFactor; }
    public int GetnumberOfEnemies() { return numberOfEnemies; }
    public float GetmoveSpeed() { return moveSpeed; }
}
