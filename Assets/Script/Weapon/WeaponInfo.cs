using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

    // ID
    public WEAPON_ID weaponID = WEAPON_ID.NONE;

    // Skill
    public SKILL_ID skillID = SKILL_ID.NONE;

    // 見た目を変更
    public void ChangeDraw()
    {
        // TODO メッシュを変更する。

        // material変更
        Material material = WeaponManager.GetWeaponMaterial(weaponID, true);

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
