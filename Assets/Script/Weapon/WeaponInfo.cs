using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

    // ID
    [SerializeField]
    WEAPON_ID weaponID = WEAPON_ID.NULL;

    // Skill
    [SerializeField]
    SKILL_ID skillID = SKILL_ID.NULL;

    WeaponParam param = new WeaponParam();

    //  -----------------------------------------------------
    //  公開用関数
    //  -----------------------------------------------------

    public WeaponParam GetParam() { return param; }
    public SKILL_ID GetSkillID() { return skillID; }

    public void Setup()
    {
        param = WeaponDatabase.GetWeapon(weaponID);
        param.skillID = skillID;
    }

    // 見た目を変更
    public void ChangeDraw()
    {
        // TODO メッシュを変更する。

        // material変更
        Material material = WeaponDatabase.LoadWeaponMaterial(weaponID, true);

        if (material == null)
        {
            Debug.Log("エラー");
            return;
        }

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material = material;

        Debug.Log("変更完了");
    }
}
