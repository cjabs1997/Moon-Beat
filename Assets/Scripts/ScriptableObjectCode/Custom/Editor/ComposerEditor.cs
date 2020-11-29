using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Composer))]
public class ComposerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Composer composer = (Composer)target;

        if(GUILayout.Button("Start Song"))
        {
            composer.startSong();
        }
    }
}
