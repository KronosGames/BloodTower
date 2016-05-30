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

    // 体力の初期化
    static public void InitHp()
    {
        SetHp(GetMaxHp());
    }

    // Staminaの初期化
    static public void InitStamina()
    {
        SetStamina(GetMaxStamina());
    }

    static public void SetHp(int hp)
    {
        hp = Mathf.Min(hp, characterParam.maxHP);
        hp = Mathf.Max(hp, 0);

        characterParam.hp = hp;
    }

    static public void SetMaxHp(int maxHp)
    {
        characterParam.maxHP = maxHp;
    }

    static public void SetStamina(int stamina)
    {
        characterParam.stamina = stamina;
    }

    static public void SetMaxStamina(int maxStamina)
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
        equipWeaponParam[index] = new WeaponParam();
        equipWeaponParam[index].id = WEAPON_ID.NULL;
        equipWeaponParam[index].skillID = SKILL_ID.NULL;

        characterParam.equipWeaponID[index] = -1;
        characterParam.equipWeaponSkillID[index] = -1;
    }

    // 生きているかどうか
    static public bool IsAlive() { return GetHp() >= 1; }

    static public int GetClubLevel() { return characterParam.clubLevel; }
    static public int GetDaggerLevel() { return characterParam.daggerLevel; }
    static public int GetSpearLevel() { return characterParam.spearLevel; }
    static public int GetSwordLevel() { return characterParam.swordLevel; }

    static public int GetHolyWaterNum() { return characterParam.holyWaterNum; }
    static public int GetMaxHolyWaterNum() { return characterParam.maxHolyWaterNum; }
    static public int GetHp() { return characterParam.hp; }
    static public int GetMaxHp() { return characterParam.maxHP; }
    static public int GetStamin() { return characterParam.stamina; }
    static public int GetMaxStamina() { return characterParam.maxStamina; }
    static public int GetAttackPower() { return characterParam.attack; }
    static public int GetDefense() { return characterParam.defense; }
    static public int GetMoveSpeed() { return characterParam.moveSpeed; }
    static public int GetAttackSpeed() { return characterParam.attackSpeed; }

    static public int GetOffsetHp()
    {
        int offset = 0;
        WeaponParam[] weaponParamList = GetEquipWeaponParam();
        for (int i = 0; i < weaponParamList.Length; i++)
        {
            if (weaponParamList[i] == null) continue;

            offset += weaponParamList[i].hp;
        }

        return offset;
    }

    static public int GetOffsetAttackPower()
    {
        int offset = 0;
        WeaponParam[] weaponParamList = GetEquipWeaponParam();
        for (int i = 0; i < weaponParamList.Length; i++)
        {
            if (weaponParamList[i] == null) continue;

            offset += weaponParamList[i].attack;
        }

        return offset;
    }

    static public int GetOffsetDefense()
    {
        int offset = 0;
        WeaponParam[] weaponParamList = GetEquipWeaponParam();
        for (int i = 0; i < weaponParamList.Length; i++)
        {
            if (weaponParamList[i] == null) continue;

            offset += weaponParamList[i].defence;
        }

        return offset;
    }

    static public int GetOffsetAttackSpeed()
    {
        int offset = 0;
        WeaponParam[] weaponParamList = GetEquipWeaponParam();
        for (int i = 0; i < weaponParamList.Length; i++)
        {
            if (weaponParamList[i] == null) continue;

            offset += weaponParamList[i].attackSpeed;
        }

        return offset;
    }

    static public int GetOffsetMoveSpeed()
    {
        int offset = 0;
        WeaponParam[] weaponParamList = GetEquipWeaponParam();
        for (int i = 0; i < weaponParamList.Length; i++)
        {
            if (weaponParamList[i] == null) continue;

            offset += weaponParamList[i].moveSpeed;
        }

        return offset;
    }

    static public int GetOffsetStamina()
    {
        int offset = 0;
        WeaponParam[] weaponParamList = GetEquipWeaponParam();
        for (int i = 0; i < weaponParamList.Length; i++)
        {
            if (weaponParamList[i] == null) continue;

            offset += weaponParamList[i].stamina;
        }

        return offset;
    }
    static public WeaponParam[] GetEquipWeaponParam()
    {
        return equipWeaponParam;
    }
}

