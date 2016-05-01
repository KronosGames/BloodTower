// -----------------------------------------
// Weapon データID
// -----------------------------------------
using UnityEngine;
public enum WEAPON_TYPE
{
    NULL = -1,     //< NULL
    ROD = 0,     //< 棒
    SWORD = 1,     //< 剣
    DAGGER = 2,     //< 短剣
    SPEAR = 3,     //< 槍
}

public enum WEAPON_ID
{
    SWORD = 20,     //< 刀
    CLUB = 0,     //< クラブ
    DAGGER = 40,     //< ダガー
    SPEAR = 60,     //< スピア
    NULL = -1,     //< NULL
}

public class WeaponParam
{
    public WEAPON_ID id;
    public WEAPON_TYPE type;
    public SKILL_ID skillID;
    public string name;
    public string explain;
    public string iconPath;
    public string materialPath;
    public int hp;
    public int attack;
    public int defence;
    public int attackSpeed;
    public int moveSpeed;
    public int stamina;
    public int durability;
    public int durabilityMax;
}
