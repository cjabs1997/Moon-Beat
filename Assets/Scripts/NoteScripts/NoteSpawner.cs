using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> notePrefab;
    public List<GameObject> longNotePrefab;
    public List<Transform> spawnPoints;


    private void SpawnNote(Tuple<float, int, string, float> noteInfo)
    {
        if (noteInfo.Item2 >= notePrefab.Count)
            return;
        if(noteInfo.Item4 != 0)
        {
            return; // Remove this and uncomment to try the long bois, they still don't work (:
            //GameObject note = GameObject.Instantiate(longNotePrefab[noteInfo.Item2], spawnPoints[noteInfo.Item2].position, Quaternion.identity);
            //note.GetComponentInChildren<LongNoteController>().InitNote(noteInfo.Item4);
        }  
        else
        {
            GameObject.Instantiate(notePrefab[noteInfo.Item2], spawnPoints[noteInfo.Item2].position, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        Composer.GenNoteCallback += SpawnNote;
    }

    private void OnDisable()
    {
        Composer.GenNoteCallback -= SpawnNote;
    }
}
