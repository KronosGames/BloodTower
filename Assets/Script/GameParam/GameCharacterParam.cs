using UnityEngine;
using System.Collections;

// キャラクター(プレイヤー用) パラメーター
public static class GameCharacterParam
{
    static CharacterTable characterParam = new CharacterTable();

    static public void Setup(ref CharacterTable table)
    {
        characterParam = table;
    }

    static public void SetHp(int hp)
    {
        characterParam.hp = hp;
    }

    static public void SetMaxHp(int maxHp)
    {
        characterParam.maxHP = maxHp;
    }

    static public void SetStamin(int stamina)
    {
        characterParam.stamina = stamina;
    }

    static public void SetMaxStamin(int maxStamina)
    {
        characterParam.maxStamina = maxStamina;
    }
    static public void SetAttackPower(int attack)
    {
        characterParam.attack = attack;
    }

    static public void SetDefense(int defense)
    {
        characterParam.defense = defense;
    }

    static public void SetMoveSpeed(int moveSpeed)
    {
        characterParam.moveSpeed = moveSpeed;
    }

    static public void SetAttackSpeed(int attackSpeed)
    {
        characterParam.attackSpeed = attackSpeed;
    }

    static public void SetHolyWaterNum(int holyWaterNum)
    {
        characterParam.holyWaterNum = holyWaterNum;
    }

    static public void SetMaxHolyWaterNum(int maxHolyWaterNum)
    {
        characterParam.maxHolyWaterNum = maxHolyWaterNum;
    }



    static public int GetHolyWaterNum() { return characterParam.holyWaterNum; }
    static public int GetMaxHolyWaterNum() { return characterParam.maxHolyWaterNum; }
    static public int GetHp() { return characterParam.hp; }
    static public int GetMaxHp() { return characterParam.maxHP; }
    static public int GetStamin() { return characterParam.stamina; }
    static public int GetMaxStamin() { return characterParam.maxStamina; }
    static public int GetAttackPower() { return characterParam.attack; }
    static public int GetDefense() { return characterParam.defense; }
    static public int GetMoveSpeed() { return characterParam.moveSpeed; }
    static public int GetAttackSpeed() { return characterParam.attackSpeed; }

}
