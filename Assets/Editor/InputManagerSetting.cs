#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(InputManager))]
public class InputManagerSetting : Editor 
{
    InputManagerGenerator generator = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InputManager manager = target as InputManager;

        List<InputSettingData> dataList = manager.GetInputSettingList();
        
        //for (int i = 0; i < dataList.Count; i++)
        //{
        //    InputSettingData data = dataList[i];

        //    using (new EditorGUILayout.HorizontalScope())
        //    {
        //        data.isFoldout = EditorGUILayout.Foldout(data.isFoldout, data.name);

        //        if (GUILayout.Button("削除",GUILayout.Width(50.0f)))
        //        {
        //            dataList.RemoveAt(i);
        //            return;
        //        }
        //    }

        //    if (data.isFoldout)
        //    {
        //        data.id = (INPUT_ID)EditorGUILayout.EnumPopup("ID : ", data.id);
        //        data.name = EditorGUILayout.TextField("Name : ", data.name);
        //        data.negative = EditorGUILayout.TextField("Negative : ", data.negative);
        //        data.positive = EditorGUILayout.TextField("Positive : ", data.positive);
        //        data.altNegative = EditorGUILayout.TextField("AltNegative : ", data.altNegative);
        //        data.altPositive = EditorGUILayout.TextField("AltPositive : ", data.altPositive);
        //        data.axisNum = (AXIS_NUM)EditorGUILayout.EnumPopup("AxisNum : ", data.axisNum);
        //        data.joyStickNum = EditorGUILayout.IntField("JoystickNum : ", data.joyStickNum);
        //        data.isJoyPadAxisCreate = EditorGUILayout.Toggle("JoypadAxisを作成する？ : ", data.isJoyPadAxisCreate);
        //        data.isMouseMovementCreate = EditorGUILayout.Toggle("MouseAxisを作成する？ : ", data.isMouseMovementCreate);

        //    }
        //}

        //if (GUILayout.Button("追加"))
        //{
        //    dataList.Add(new InputSettingData());
        //}

        if (GUILayout.Button("反映"))
        {
            generator = new InputManagerGenerator();

            generator.Clear();

            for (int i = 0; i < dataList.Count; i++)
            {
                InputSettingData data = dataList[i];

                AxisType type = data.isMouseMovementCreate ? AxisType.MouseMovement : AxisType.KeyOrMouseButton;
                generator.AddAxis(InputAxis.CreateKeyAxis(data.name, data.negative, data.positive, type, data.altNegative,data.altPositive));

                if (data.isJoyPadAxisCreate)
                {
                    generator.AddAxis(InputAxis.CreatePadAxis(data.name, data.joyStickNum, (int)data.axisNum));
                }
            }

            Debug.Log("反映成功");
        }
    }



}


/// <summary>
/// 軸のタイプ
/// </summary>
public enum AxisType
{
    KeyOrMouseButton = 0,
    MouseMovement = 1,
    JoystickAxis = 2
};

/// <summary>
/// 入力の情報
/// </summary>
public class InputAxis
{
    public string name = "";
    public string descriptiveName = "";
    public string descriptiveNegativeName = "";
    public string negativeButton = "";
    public string positiveButton = "";
    public string altNegativeButton = "";
    public string altPositiveButton = "";

    public float gravity = 0;
    public float dead = 0;
    public float sensitivity = 0;

    public bool snap = false;
    public bool invert = false;

    public AxisType type = AxisType.KeyOrMouseButton;

    // 1から始まる。
    public int axis = 1;

    // 0なら全てのゲームパッドから取得される。1以降なら対応したゲームパッド。
    public int joyNum = 0;

    /// <summary>
    /// ゲームパッド用の軸の設定データを作成する
    /// </summary>
    /// <returns>The joy axis.</returns>
    /// <param name="name">Name.</param>
    /// <param name="joystickNum">Joystick number.</param>
    /// <param name="axisNum">Axis number.</param>
    public static InputAxis CreatePadAxis(string name, int joystickNum, int axisNum)
    {
        var axis = new InputAxis();
        axis.name = name;
        axis.dead = 0.2f;
        axis.gravity = 3;
        axis.sensitivity = 1.0f;
        axis.type = AxisType.JoystickAxis;
        axis.axis = axisNum;
        axis.joyNum = joystickNum;

        return axis;
    }

    /// <summary>
    /// キーボード用の軸の設定データを作成する
    /// </summary>
    /// <returns>The key axis.</returns>
    /// <param name="name">Name.</param>
    /// <param name="negativeButton">Negative button.</param>
    /// <param name="positiveButton">Positive button.</param>
    /// <param name="axisNum">Axis number.</param>
    public static InputAxis CreateKeyAxis(string name, string negativeButton, string positiveButton, 
        AxisType type, string altNegativeButton = "", string altPositiveButton = "")
    {
        var axis = new InputAxis();
        axis.name = name;
        axis.negativeButton = negativeButton;
        axis.positiveButton = positiveButton;
        axis.altNegativeButton = altNegativeButton;
        axis.altPositiveButton = altPositiveButton;
        axis.gravity = 3;
        axis.sensitivity = 1.0f;
        axis.dead = 0.2f;
        axis.type = type;

        return axis;
    }
}

/// <summary>
/// InputManagerを設定するためのクラス
/// </summary>
public class InputManagerGenerator
{

    SerializedObject serializedObject;
    SerializedProperty axesProperty;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public InputManagerGenerator()
    {
        Object[] assetAll = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset");
        serializedObject = new SerializedObject(assetAll[0]);
        axesProperty = serializedObject.FindProperty("m_Axes");
    }

    /// <summary>
    /// 軸を追加します。
    /// </summary>
    /// <param name="serializedObject">Serialized object.</param>
    /// <param name="axis">Axis.</param>
    public void AddAxis(InputAxis axis)
    {
        if (axis.axis < 1) Debug.LogError("Axisは1以上に設定してください。");
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.arraySize++;
        serializedObject.ApplyModifiedProperties();

        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
        GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
        GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
        GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

        serializedObject.ApplyModifiedProperties();

    }

    /// <summary>
    /// 子要素のプロパティを取得します。
    /// </summary>
    /// <returns>The child property.</returns>
    /// <param name="parent">Parent.</param>
    /// <param name="name">Name.</param>
    private SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }

    /// <summary>
    /// 設定を全てクリアします。
    /// </summary>
    public void Clear()
    {
        axesProperty.ClearArray();
        serializedObject.ApplyModifiedProperties();
    }
}

#endif