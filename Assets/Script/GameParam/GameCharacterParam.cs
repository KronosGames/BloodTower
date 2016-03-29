using UnityEngine;
using System.Collections;

// キャラクター(プレイヤー用) パラメーター
public static class GameCharacterParam
{
    static CharacterTable characterParam = null;

    static public void Setup()
    {
        characterParam = CharacterDatabase.GetCharacterInfo();
    }

    static public void SetHp(int hp)
    {
        characterParam.hp = hp;
    }

    static public void SetMaxHp(int maxHp)
    {
        characterParam.maxHP = maxHp;
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

    static public int GetHp() { return characterParam.hp; }
    static public int GetMaxHp() { return characterParam.maxHP; }
    static public int GetAttackPower() { return characterParam.attack; }
    static public int GetDefense() { return characterParam.defense; }
    static public int GetMoveSpeed() { return characterParam.moveSpeed; }
    static public int GetAttackSpeed() { return characterParam.attackSpeed; }

}
