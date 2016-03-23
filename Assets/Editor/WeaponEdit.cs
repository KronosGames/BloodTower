#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WeaponInfo))]
public class WeaponEdit : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("反映"))
        {
            WeaponInfo data = target as WeaponInfo;
            data.ChangeDraw();
        }
    }

}

#endif