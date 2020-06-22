using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelGenerationEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Level level = target as Level;

        if (DrawDefaultInspector())
        {
            level.GenerateLevel();
        }

        if (GUILayout.Button("GenerateLevel"))
        {
            level.GenerateLevel();
        }
    }

}
