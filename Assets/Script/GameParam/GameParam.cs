﻿using UnityEngine;
using System.Collections;

public class GameParam : MonoBehaviour
{
    static GameUserTable gameUserData = null;

    void Start()
    {
        gameUserData = GameUserDatabase.GetGameUserInfo();
    }

    static public void SetUserName(string newName)
    {
        gameUserData.name = newName;
    }

    static public string GetUserName()
    {
        return gameUserData.name;
    }

    static public void SetItemID(int index,int itemID)
    {
        if (gameUserData.itemList.Length <= index) return;
        if (-1 >= index) return;

        gameUserData.itemList[index] = itemID;
    }

    static public int GetItemID(int index)
    {
        if (gameUserData.itemList.Length <= index) return -1;
        if (-1 >= index) return - 1;

        return gameUserData.itemList[index];
    }

    static public int[] GetItemIDList()
    {
        return gameUserData.itemList;
    }


}
