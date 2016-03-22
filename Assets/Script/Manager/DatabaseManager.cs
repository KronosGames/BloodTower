using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DB_ID
{ 
    NONE,
    CHARACTER,
    WEAPON,
    SKILL
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
        databseList.Add(GetData(DB_ID.WEAPON, "WeaponList" ,"WeaponTable"));
        databseList.Add(GetData(DB_ID.SKILL, "SkillList" ,"SkillTable"));
    }

    void Start()
    {
        InitManager(this, MANAGER_ID.DATABASE);
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
