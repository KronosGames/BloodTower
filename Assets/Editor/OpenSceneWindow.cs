using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class OpenSceneWindow : EditorWindow {

    const string MENU_PATH = "OpenScene/";

    [MenuItem(MENU_PATH + "BloodTower")]
    static void BloodTowerSceneOpen()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scene/BloodTower.unity");
    }

    [MenuItem(MENU_PATH + "EntryScene")]
    static void EntrySceneOpen()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scene/Entry.unity");
    }

    [MenuItem(MENU_PATH + "HomeScene")]
    static void HomeSceneOpen()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scene/Home.unity");
    }

    [MenuItem(MENU_PATH + "BattleMapScene")]
    static void BattleMapSceneOpen()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scene/BattleMap.unity");
    }

    [MenuItem(MENU_PATH + "CampScene")]
    static void CampSceneOpen()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scene/Camp.unity");
    }
}
