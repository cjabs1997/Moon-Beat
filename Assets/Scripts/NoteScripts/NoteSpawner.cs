using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> notePrefab;
    public List<Transform> spawnPoints;

    void Start()
    {
        Composer.GenNoteCallback += SpawnNote;
    }


    void Update()
    {
        
    }

    private void SpawnNote(Tuple<float, int, string, float> noteInfo)
    {
        if (noteInfo.Item2 >= notePrefab.Count)
            return;
        GameObject.Instantiate(notePrefab[noteInfo.Item2], spawnPoints[noteInfo.Item2].position, Quaternion.identity);  
    }
}
