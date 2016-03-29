using UnityEngine;
using System.Collections;

public enum EQUIP_WEAPON_TYPE : byte
{
    MAIN = 0,
    SUB = 1,
}

// キャラクター(プレイヤー用) パラメーター
public static class GameCharacterParam
{
    const int EQUIP_MAX = 2;
    static CharacterTable characterParam = new CharacterTable();
    static WeaponParam[] equipWeaponParam = new WeaponParam[EQUIP_MAX];

    static public void Setup(ref CharacterTable table)
    {
        characterParam = table;

        for (int i = 0; i < EQUIP_MAX; i++)
        {
            WeaponParam dataWeapon = new WeaponParam();

            WEAPON_ID equipWeaponID = (WEAPON_ID)characterParam.equipWeaponID[i];
            SKILL_ID equipWeaponSkillID = (SKILL_ID)characterParam.equipWeaponSkillID[i];

            dataWeapon = WeaponDatabase.GetWeapon(equipWeaponID);
            if (dataWeapon == null)
            {
                equipWeaponParam[i] = new WeaponParam();
                equipWeaponParam[i].id = WEAPON_ID.NULL;
                equipWeaponParam[i].skillID = SKILL_ID.NULL;
                continue;
            }

            dataWeapon.skillID = equipWeaponSkillID;
            equipWeaponParam[i] = dataWeapon;
        }
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

    // 装備武器を設定する。
    static public void SetEquipWeapon(EQUIP_WEAPON_TYPE type, ref WeaponParam param)
    {
        int index = (int)type;
        equipWeaponParam[index] = param;

        characterParam.equipWeaponID[index] = (int)param.id;
        characterParam.equipWeaponSkillID[index] = (int)param.skillID;
    }

    // 装備武器を捨てる
    static public void RemoveEquipWeapon(EQUIP_WEAPON_TYPE type)
    {
        int index = (int)type;
        equipWeaponParam[index] = null;

        characterParam.equipWeaponID[index] = -1;
        characterParam.equipWeaponSkillID[index] = -1;
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

    static public WeaponParam[] GetEquipWeaponParam()
    {
        return equipWeaponParam;
    }
}
