using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossEnemyDatabase : MonoBehaviour {

    static List<BossEnemyParam> paramList = new List<BossEnemyParam>();

    void Start()
    {
        paramList.Clear();

        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.BOSS_ENEMY);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            BossEnemyParam addData = new BossEnemyParam();

            addData.id = (BOSS_ENEMY_ID)data.GetInt("id");
            addData.hp = data.GetInt("hp");
            addData.attack = data.GetInt("attack");
            addData.defense = data.GetInt("defense");
            addData.moveSpeed = data.GetInt("moveSpeed");
            addData.name = data.GetString("name");
            addData.maxHp = addData.hp;

            paramList.Add(addData);
        }
    }


    //  ---------------------------------------------
    //  公開用関数
    //  ---------------------------------------------

    static public BossEnemyParam GetBossEnemyData(BOSS_ENEMY_ID id)
    {
        for (int i = 0; i < paramList.Count; i++)
        {
            BossEnemyParam data = paramList[i];
            if (data.id == id)
            {
                return data;
            }
        }

        return null;
    }

}
