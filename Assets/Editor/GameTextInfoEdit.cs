#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GameTextInfo))]
public class GameTextInfoEdit : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("適応"))
        {
            GameTextInfo data = target as GameTextInfo;
            data.Apply();

            EditorApplication.SaveAssets();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
    }

}

#endif