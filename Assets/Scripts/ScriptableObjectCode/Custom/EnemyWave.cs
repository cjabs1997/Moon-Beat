using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [Header("Wave Enemies")]
    [Tooltip("The list of prefabs to spawn. Will spawn the enemies in the order of the list. Should be of equal length to numToSpawn.")]
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    [Tooltip("How many of a prefab to spawn. The prefab to spawn is the one which shares the same index in the enemyPrefabs list. Should be of equal length to enemyPrefabs.")]
    public List<int> numToSpawn = new List<int>();

    [Header("Wave traits")]
    [Tooltip("The interval to which enemies will be spawn (every spawnInterval seconds).")]
    public float spawnInterval = 0.01f;
}