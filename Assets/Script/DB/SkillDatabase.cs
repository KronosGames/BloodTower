using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// スキルID
public enum SKILL_ID
{
    NONE = -1,
    FIRE_OUTPUT,   //< 炎を出す
    ATK_UP,         //< 攻撃力アップ
}


public class SkillTable
{
    public int id;
    public string name;
    public string explain;
}

public class SkillDatabase : MonoBehaviour
{

    static List<SkillTable> skillList = new List<SkillTable>();

	void Start ()
    {
        skillList.Clear();

        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.SKILL);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            SkillTable addData = new SkillTable();

            addData.id = data.GetInt("id");
            addData.name = data.GetString("name");
            addData.explain = data.GetString("explain");

            skillList.Add(addData);
        }
    }


    //  --------------------------------------------
    //  公開関数
    //  --------------------------------------------

    static public SkillTable GetSkillByName(string name)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].name == name)
            {
                return skillList[i];
            }
        }

        return null;
    }

    static public SkillTable GetSkillById(int id)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].id == id)
            {
                return skillList[i];
            }
        }

        return null;
    }

}
