using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDatabase : MonoBehaviour
{
    static List<EnemyParam> paramList = new List<EnemyParam>();

    void Start()
    {
        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.ENEMY);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            EnemyParam addData = new EnemyParam();

            addData.id = (ENEMY_ID)data.GetInt("id");
            addData.hp = data.GetInt("hp");
            addData.attack = data.GetInt("attack");
            addData.defense = data.GetInt("defense");
            addData.moveSpeed = data.GetInt("moveSpeed");
            addData.name = data.GetString("name");
            addData.maxHp = addData.hp;

            paramList.Add(addData);
        }
    }


    //  --------------------------------------------------
    //  公開用 関数
    //  --------------------------------------------------

    static public EnemyParam GetEnemyParam(ENEMY_ID id)
    {
        for (int i = 0; i < paramList.Count; i++)
        {
            if (paramList[i].id == id)
            {
                return paramList[i];
            }
        }

        return null;
    }

}
