using UnityEngine;
using System.Collections;

public class BattleManager : ManagerBase
{
    enum SEQ_STATE
    {
        NONE,
        CHANGE_MAP,
        UPDATE,
        YOU_DIED,
    }

    [SerializeField]
    float youDiedWaitTime = 3.0f;

    static BattleManager myThis = null;

    MapInfoData currentMapData = null;
    SEQ_STATE seqState = SEQ_STATE.NONE;
    BattleMapScene.CHANGE_SCENE_TYPE changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.NONE;
    float youDiedTime = 0.0f;
    int currentCount = 0;

    void Start ()
    { 
        InitManager(this, MANAGER_ID.BATTLE);

        myThis = this;
    }

    // マップ情報を変更
    static void ChangeMapData()
    {
        if (myThis.currentMapData != null)
        {
            myThis.currentMapData.trans.gameObject.SetActive(false);
        }

        myThis.currentMapData = MapManager.GetMapInfo(myThis.currentCount);
        myThis.currentMapData.trans.gameObject.SetActive(true);

        // 子オブジェクトにある武器すべてをセットアップする。
        WeaponInfo[] weaponList = myThis.currentMapData.trans.GetComponentsInChildren<WeaponInfo>();
        WeaponManager.Setup(ref weaponList);

        // ボス
        BossEnemyInfo bossEnemyInfo = myThis.currentMapData.trans.GetComponentInChildren<BossEnemyInfo>();
        BossEnemyManager.Setup(ref bossEnemyInfo);

        // エネミーリスト
        EnemyInfo[] enemyList = myThis.currentMapData.trans.GetComponentsInChildren<EnemyInfo>();
        EnemyManager.Setup(ref enemyList);
    }

    void Update()
    {
        switch (seqState)
        {
            case SEQ_STATE.CHANGE_MAP:
                ChangeMapData();
                myThis.seqState = SEQ_STATE.UPDATE;
                break;
            case SEQ_STATE.UPDATE:
                // TEST CODE
                if (Input.GetKeyDown(KeyCode.L))
                {
                    UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.YOU_DIED_INFO);
                    myThis.seqState = SEQ_STATE.YOU_DIED;
                    youDiedTime = youDiedWaitTime;
                }
                break;
            case SEQ_STATE.YOU_DIED:
                youDiedTime -= Time.deltaTime;
                if (youDiedTime <= 0.0f)
                {
                    myThis.changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.HOME;
                }
                break;
        }
    }

    //  ----------------------------------------------
    //  公開用 関数
    //  ----------------------------------------------

    // 初期化
    static public void Setup()
    {
        myThis.currentCount = 0;
        myThis.currentMapData = null;
        myThis.seqState = SEQ_STATE.UPDATE;
        myThis.changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.NONE;

        ChangeMapData();
    }


    // 上の階層に行くときに呼んでください。
    static public void ChangeUpFloor()
    {
        if (MapManager.GetMapCount() - 1 <= myThis.currentCount) return;

        myThis.seqState = SEQ_STATE.CHANGE_MAP;
        myThis.currentCount++;

        // キャンプに行く条件文を記述する。
        //if()
        //{
        //    changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.CAMP;
        //}
    }

    // 下の階層、行く時に呼んでください。
    static public void ChangeDownFloor()
    {
        myThis.seqState = SEQ_STATE.CHANGE_MAP;
        myThis.currentCount--;

        if (0 >= myThis.currentCount)
        {
            myThis.currentCount = 0;
            myThis.changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.HOME;
        }
    }

    // 現在の階層マップデータ
    static public MapInfoData GetCurrentMapData()
    {
        return myThis.currentMapData;
    }

    // 切り替えるシーンの種類を取得
    // 通知用に使います。
    static public BattleMapScene.CHANGE_SCENE_TYPE GetChangeSceneType()
    {
        return myThis.changeSceneType;
    }


}
