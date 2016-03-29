using UnityEngine;
using System.Collections;

public class CharacterTable
{
    public int id;
    public int hp;
    public int maxHP;
    public int attack;
    public int defense;
    public int attackSpeed;
    public int moveSpeed;
}

public class CharacterDatabase : MonoBehaviour
{
    static CharacterTable characterTable = new CharacterTable();

    void Start()
    {
        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.CHARACTER);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            CharacterTable tableData = new CharacterTable();

            tableData.id = data.GetInt("id");
            tableData.hp = data.GetInt("hp");
            tableData.attack = data.GetInt("attack");
            tableData.defense = data.GetInt("defence");
            tableData.attackSpeed = data.GetInt("attackSpeed");
            tableData.moveSpeed = data.GetInt("moveSpeed");
            tableData.maxHP = tableData.hp;

            characterTable = tableData;
        }

        GameCharacterParam.Setup();
    }



    //  -------------------------------------------------
    //  公開用関数
    //  -------------------------------------------------

    // キャラクター情報を取得
    static public CharacterTable GetCharacterInfo()
    {
        return characterTable;
    }

}
