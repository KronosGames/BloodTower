using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;


public class AnimationDBEditor : EditorWindow {

    const string TOOL_NAME = "AnimationDBCreator";
    const string ASSET_PATH = "Assets/Resources/Animation/Data/";
    const string SCRIPT_PATH = "Assets/Script/DB/Animation/";

    Vector2 scrollViewPosition = new Vector2();
    static AnimationDatabase animationDatabase = null;
    static AnimationDatabase loadAnimationDatabase = null;
    static string assetName = "";

    [MenuItem("Tools/" + TOOL_NAME)]
    static void Open()
    {
        EditorWindow window = EditorWindow.GetWindow<AnimationDBEditor>();
        window.titleContent = new GUIContent(TOOL_NAME);
        window.Show();
        window.minSize = new Vector2(400.0f, 400.0f);
        animationDatabase = null;
        loadAnimationDatabase = null;
        assetName = "";
    }


    void CreateScript()
    {
        using (var sw = new System.IO.StreamWriter(SCRIPT_PATH + animationDatabase.assetName + ".cs"))
        {
            
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("");

            sw.WriteLine("// -----------------------------------------");
            sw.WriteLine("// AnimationData");
            sw.WriteLine("// -----------------------------------------");
            sw.WriteLine("");

            sw.WriteLine("public class " + animationDatabase.assetName + "Param");
            sw.WriteLine("{");

            sw.WriteLine("    public enum ANIMATION_ID");
            sw.WriteLine("    {");

            List<AnimationData> animationList = animationDatabase.animationList;
            for (int i = 0; i < animationList.Count; i++)
            {
                if (animationList[i].labelName == "")
                {
                    animationList[i].labelName = animationList[i].clip.name;
                }
                sw.WriteLine("        " + animationList[i].labelName + ",");
            }

            sw.WriteLine("    }");

            sw.WriteLine("    public ANIMATION_ID id;");
            sw.WriteLine("    public AnimationClip clip;");
            sw.WriteLine("    public float blendTime;");
            sw.WriteLine("    public bool isBlend;");

            sw.WriteLine("}");
        }

        AssetDatabase.Refresh();

        Debug.Log("スクリプト出力成功");
    }


    void OnGUI()
    {
        loadAnimationDatabase = EditorGUILayout.ObjectField("Database : ", loadAnimationDatabase, typeof(AnimationDatabase), false) as AnimationDatabase;

        assetName = EditorGUILayout.TextField("アセット名 : ", assetName);
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("新規ファイル"))
            {
                assetName = "";
                loadAnimationDatabase = null;
                animationDatabase = null;
                Debug.Log("新規ファイルにしました");
            }

            if (GUILayout.Button("読み込み"))
            {
                if (loadAnimationDatabase == null)
                {
                    Debug.LogError("読み込み失敗");
                    return;
                }

                animationDatabase = loadAnimationDatabase;
                assetName = animationDatabase.assetName;
            }

            if (GUILayout.Button("アセット作成"))
            {
                if (assetName == "")
                {
                    Debug.LogError("アセット名が空です。入力をしてもう一度押してください！");
                    return;
                }

                animationDatabase = ScriptableObject.CreateInstance<AnimationDatabase>(); ;
                animationDatabase.assetName = assetName;
                AssetDatabase.CreateAsset(animationDatabase, ASSET_PATH + animationDatabase.assetName + ".asset");
                loadAnimationDatabase = animationDatabase;
                Debug.Log("作成されました");
            }

            if (GUILayout.Button("アセット保存"))
            {
                if (animationDatabase == null)
                {
                    Debug.LogError("アセットが空です。");
                    return;
                }

                CreateScript();
                AssetDatabase.SaveAssets();
                Debug.Log("保存しました。");
            }
        }
        EditorGUILayout.EndHorizontal();

        if (animationDatabase == null) return;

        GUILayout.Space(20.0f);

        EditorGUILayout.LabelField("登録アニメーション数 ; " + animationDatabase.animationList.Count);
        scrollViewPosition = EditorGUILayout.BeginScrollView(scrollViewPosition);
        {
            List<AnimationData> animationList = animationDatabase.animationList;
            for (int i = 0; i < animationList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    animationList[i].isFold = EditorGUILayout.Foldout(animationList[i].isFold, animationList[i].labelName);
                    if (GUILayout.Button("Remove",GUILayout.Width(100.0f)))
                    {
                        animationList.RemoveAt(i);
                        return;
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (animationList[i].isFold)
                {
                    animationList[i].labelName = EditorGUILayout.TextField("ID : ", animationList[i].labelName);
                    animationList[i].clip = EditorGUILayout.ObjectField("AnimationClip : ", animationList[i].clip, typeof(AnimationClip), false) as AnimationClip;
                    animationList[i].isBlend = EditorGUILayout.Toggle("Blendするかどうか : ", animationList[i].isBlend);
                    if (animationList[i].isBlend)
                    {
                        animationList[i].blendTime = EditorGUILayout.FloatField("BlendTime : ", animationList[i].blendTime);
                    }
                }

                GUILayout.Space(10.0f);
            }
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("追加"))
        {
            animationDatabase.animationList.Add(new AnimationData());
        }
    }

}
