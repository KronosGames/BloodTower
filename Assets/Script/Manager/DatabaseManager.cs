using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DB_ID
{ 
    NONE,
    GAMEUSER,
    CHARACTER,
    WEAPON,
    ITEM,
    SKILL,
	GAMETEXT,
    BOSS_ENEMY,
    ENEMY,
}

public class DatabaseLoadData
{
    public DataTable dataTable = null;
    public DB_ID id = DB_ID.NONE;
    public bool isLoaded = false;
    public string dbName = "";
    public string tableName = "";
}

public class DatabaseManager : ManagerBase 
{
    static List<DatabaseLoadData> databseList = new List<DatabaseLoadData>();

    DatabaseLoadData GetData(DB_ID id,string dbName,string tableName)
    {
        DatabaseLoadData data = new DatabaseLoadData();
        data.dbName = dbName;
        data.tableName = tableName;
        data.id = id;

        return data;
    }

    void Awake()
    {
        databseList.Add(GetData(DB_ID.GAMEUSER, "GameUser" ,"GameUserTable"));
        databseList.Add(GetData(DB_ID.CHARACTER, "Character" ,"CharacterTable"));
        databseList.Add(GetData(DB_ID.WEAPON, "WeaponList" ,"WeaponTable"));
        databseList.Add(GetData(DB_ID.SKILL, "SkillList" ,"SkillTable"));
        databseList.Add(GetData(DB_ID.GAMETEXT, "GameText" , "GameTextTable"));
        databseList.Add(GetData(DB_ID.BOSS_ENEMY, "BossEnemyList", "BossEnemyTable"));
        databseList.Add(GetData(DB_ID.ENEMY, "EnemyList", "EnemyTable"));
        databseList.Add(GetData(DB_ID.ITEM, "ItemList", "ItemTable"));
    }

    void Start()
    {
        InitManager(this, MANAGER_ID.DATABASE);
    }

    // デバッグ用
    public static DataTable DebugReqLoad(DB_ID id, string dbName, string tableName)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(dbName + ".db");
        string selectQuery = "SELECT * FROM " + tableName;
        return sqlDB.ExecuteQuery(selectQuery);
    }

    public static DataTable RequestLoad(DB_ID id)
    {
        for (int i = 0; i < databseList.Count; i++)
        {
            DatabaseLoadData data = databseList[i];
            if (data.id != id) continue;
            if (data.isLoaded) continue;

            SqliteDatabase sqlDB = new SqliteDatabase(data.dbName + ".db");
            string selectQuery = "SELECT * FROM " + data.tableName;
            data.dataTable = sqlDB.ExecuteQuery(selectQuery);
            
            data.isLoaded = true;

            return data.dataTable;
        }

        return null;
    }


    public static bool IsLoaded(DB_ID id)
    {
        for (int i = 0; i < databseList.Count; i++)
        {
            DatabaseLoadData data = databseList[i];
            if (data.id != id) continue;

            return data.isLoaded;
        }

        return false;
    }

}
