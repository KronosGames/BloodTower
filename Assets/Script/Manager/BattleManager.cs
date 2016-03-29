using UnityEngine;
using System.Collections;

public class BattleManager : ManagerBase
{
    static BattleMapScene.CHANGE_SCENE_TYPE changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.NONE;
    static int currentCount = 0;
    static bool isChange = false;
    static MapInfoData currentMapData = null;

	void Start ()
    {
        InitManager(this, MANAGER_ID.BATTLE);

    }

    // マップ情報を変更
    static void ChangeMapData()
    {
        if (currentMapData != null)
        {
            currentMapData.trans.gameObject.SetActive(false);
        }

        currentMapData = MapManager.GetMapInfo(currentCount);
        currentMapData.trans.gameObject.SetActive(true);

        // 子オブジェクトにある武器すべてをセットアップする。
        WeaponInfo[] weaponList = currentMapData.trans.GetComponentsInChildren<WeaponInfo>();
        WeaponManager.Setup(weaponList);
    }


    void Update()
    {
        if (isChange)
        {
            ChangeMapData();
            isChange = false;
        }

        if (InputManager.IsDown(INPUT_ID.SUBMIT))
        {
            //NotificationWindowParam param = new NotificationWindowParam();
            //param.title = "武器 取得";
            //param.name = "ドラえもん";
            //param.exlpain = "猫型ロボット製品";

            //UIEvent.OpenNotificationWindow(ref param);

            UIEvent.ChangeEquipWeapon();
        }
    }

    //  ----------------------------------------------
    //  公開用 関数
    //  ----------------------------------------------

    // 初期化
    static public void Setup()
    {
        currentCount = 0;
        currentMapData = null;
        isChange = false;
        changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.NONE;

        ChangeMapData();
    }


    // 上の階層に行くときに呼んでください。
    static public void ChangeUpFloor()
    {
        if (MapManager.GetMapCount() - 1 <= currentCount) return;

        isChange = true;
        currentCount++;

        // キャンプに行く条件文を記述する。
        //if()
        //{
        //    changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.CAMP;
        //}
    }

    // 下の階層、行く時に呼んでください。
    static public void ChangeDownFloor()
    {
        isChange = true;
        currentCount--;

        if (0 >= currentCount)
        {
            currentCount = 0;
            changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.HOME;
        }
    }

    // 現在の階層マップデータ
    static public MapInfoData GetCurrentMapData()
    {
        return currentMapData;
    }

    // 切り替えるシーンの種類を取得
    // 通知用に使います。
    static public BattleMapScene.CHANGE_SCENE_TYPE GetChangeSceneType()
    {
        return changeSceneType;
    }


}
