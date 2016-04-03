using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 入力種類のID
// 入力を追加する場合は、このENUMを追加してください。
public enum INPUT_ID
{ 
    NULL = -1,                  //< 未定義 

    // EventSystem
    HORIZONTAL,
    VERTICAL,
    SUBMIT,
    CANCEL,

    PLAYER_MOVE_HORIZONTAL,     //< 横移動
    PLAYER_MOVE_VERTICAL,       //< 縦移動
    PLAYER_ATTACK,              //< 攻撃
    PLAYER_JUMP,                //< ジャンプ
    PLAYER_GET_WEAPON,          //< 武器を拾う
    PLAYER_DROP_WEAPON,         //< 武器を捨てる
    ZOOM,                       //< ズーム
    PAUSE,                      //< ポーズ
    USE_SKILL,                  //< スキルを使う
    USE_ITEM,                   //< アイテムを使う
    ITEM_SELECT_VERTICAL,       //< アイテムの選択
    ITEM_SELECT_HORIZONTAL,     //< アイテムの選択
    SKILL_SELECT_HORIZONTAL,     //< スキルの選択
    SKILL_SELECT_VERTICAL,     //< スキルの選択
    CAMERA_ROTATE_HORIZONTAL,   //< カメラ横回転
    CAMERA_ROTATE_VERTICAL,     //< カメラ縦回転
    PLAYER_SPRINT,              //< ダッシュ
    PLAYER_ROLLING

}

public enum AXIS_NUM
{
    AXIS_X = 1,
    AXIS_Y = 2,
    AXIS_LT_RT = 3,
    AXIS_RIGHT_X = 4,
    AXIS_RIGHT_Y = 5,
    AXIS_DPAD_X = 6,
    AXIS_DPAD_Y = 7,
}

[System.Serializable]
public class InputSettingData
{
    public INPUT_ID id = INPUT_ID.NULL;
    public string name = "";
    public string negative = "";
    public string positive = "";
    public string altNegative = "";
    public string altPositive = "";

    public AXIS_NUM axisNum = AXIS_NUM.AXIS_X;
    public int joyStickNum = 0;
    public bool isMouseMovementCreate = false;
    public bool isJoyPadAxisCreate = false;

    //public bool isFoldout = false;
}

public class InputManager : ManagerBase
{
    public class InputData
    {
        public enum STATE
        {
            NONE,
            DOWN,
            PRESS,
            UP,
        }

        public string name;
        public INPUT_ID id = INPUT_ID.NULL;
        public STATE state = STATE.NONE;
    }

    [SerializeField]
    List<InputSettingData> inputSettingList = new List<InputSettingData>();

    static List<InputData> inputDataList = new List<InputData>();

    static int CompareNumber(KeyValuePair<INPUT_ID, InputData> x, KeyValuePair<INPUT_ID, InputData> y)
    {
        if ((int)x.Key > (int)y.Key)
            return 1; 
        else if ((int)x.Key < (int)y.Key)
            return -1; 
        
        return 0; 
    }


	void Start () 
    {
        InitManager(this, MANAGER_ID.INPUT);

        inputDataList.Clear();

        for (int i = 0; i < inputSettingList.Count; i++)
        {
            AddInputData(inputSettingList[i].id,inputSettingList[i].name);
        }
	}

    void AddInputData(INPUT_ID id, string name)
    {
        InputData addData = new InputData();
        addData.name = name;
        addData.id = id;
        inputDataList.Add(addData);
    }

    void Update()
    {
        for (int i = 0; i < inputDataList.Count; i++)
        {
            var data = inputDataList[i];

            switch (data.state)
            { 
                case InputData.STATE.NONE:
                    if (Input.GetAxisRaw(data.name) != 0)
                    {
                        data.state = InputData.STATE.DOWN;
                    }

                    break;
                case InputData.STATE.DOWN:
                    data.state = InputData.STATE.PRESS;
                    break;
                case InputData.STATE.PRESS:
                    if (Input.GetAxisRaw(data.name) == 0)
                    {
                        data.state = InputData.STATE.UP;
                    }

                    break;
                case InputData.STATE.UP:
                    data.state = InputData.STATE.NONE;
                    break;
            }
        }
        
    }


    static InputData GetInput(INPUT_ID id)
    {
        for (int i = 0; i < inputDataList.Count; i++)
        {
            if (inputDataList[i].id == id)
            {
                return inputDataList[i];
            }
        }

        return null;
    }


    //  -----------------------------------------
    //  公開用関数
    //  -----------------------------------------

    static public bool IsAnyDown()
    {
        return Input.anyKeyDown;
    }

    static public float GetAxis(INPUT_ID id)
    {
        return Input.GetAxis(GetInput(id).name);
    }

    static public float GetAxisRaw(INPUT_ID id)
    {
        return Input.GetAxisRaw(GetInput(id).name);
    }

    static public bool IsPress(INPUT_ID id)
    {
        return GetInput(id).state == InputData.STATE.PRESS;
    }

    static public bool IsDown(INPUT_ID id)
    {
        return GetInput(id).state == InputData.STATE.DOWN;
    }

    static public bool IsUp(INPUT_ID id)
    {
        return GetInput(id).state == InputData.STATE.UP;
    }

    public List<InputSettingData> GetInputSettingList()
    {
        return inputSettingList;
    }

}
