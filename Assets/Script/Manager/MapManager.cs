using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapInfoData
{
    public int count;
    public string name;
    public Transform trans;
}

public class MapManager : ManagerBase
{
    static List<MapInfoData> mapDataList = new List<MapInfoData>();

	void Start ()
    {
        InitManager(this, MANAGER_ID.MAP);

        mapDataList.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            MapInfoData addData = new MapInfoData();
            addData.trans = transform.GetChild(i);
            addData.name = addData.trans.name;
            addData.count = i;

            addData.trans.gameObject.SetActive(false);

            mapDataList.Add(addData);
        }
	}


    //  --------------------------------------------
    //  公開用 関数
    //  --------------------------------------------

    // マップ階層数
    static public int GetMapCount() { return mapDataList.Count; }

    static public MapInfoData GetMapInfo(int count)
    {
        if (mapDataList.Count <= count) return null;
        if (0 > count) return null;

        return mapDataList[count];
    }



}
