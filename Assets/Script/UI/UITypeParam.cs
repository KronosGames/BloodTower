using UnityEngine;
using System.Collections;


// UIの種類
public enum UI_TYPE_ID
{
    NONE,   //< 未定義

    // Common
    SCROLL_CONTENT,

    // Player
    PLAYER_INFO,
    WEAPON_INFO,
    HOLYWATER_INFO,
    PLAYER_STATUS_MENU_INFO,

    // Battle
    BOSS_ENEMY_INFO,
    NOTIFICATION_WINDOW_INFO,
    YOU_DIED_INFO,              //< 死亡した画面情報

}


// スクリーンタイプ
public enum UI_SCREEN_TYPE
{
    NONE = -1,

    BOSS_ENEMY_INFO,
    PLAYER_INFO,
    PLAYER_STATUS_MENU_INFO,
    YOU_DIED_INFO,              //< 死亡した画面情報

    MAX,
}