using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDatabase : MonoBehaviour
{

    static List<SkillParam> skillList = new List<SkillParam>();

	void Start ()
    {
        skillList.Clear();

        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.SKILL);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            SkillParam addData = new SkillParam();

            addData.id = (SKILL_ID)data.GetInt("id");
            addData.name = data.GetString("name");
            addData.explain = data.GetString("explain");
            skillList.Add(addData);
        }
    }


    //  --------------------------------------------
    //  公開関数
    //  --------------------------------------------

    static public SkillParam GetSkillByName(string name)
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

    static public SkillParam GetSkillById(SKILL_ID id)
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
