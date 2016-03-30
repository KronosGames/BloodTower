using UnityEngine;
using System.Collections;

public class BossEnemyInfo : MonoBehaviour
{
    [SerializeField]
    BOSS_ENEMY_ID id = BOSS_ENEMY_ID.NULL;

    BossEnemyParam param = null;

    public BossEnemyParam GetParam() { return param; }

    // 初期化
    public void Setup()
    {
        BossEnemyParam temp = BossEnemyDatabase.GetBossEnemyData(id);

        param = new BossEnemyParam();
        param.id = temp.id;
        param.name = temp.name;
        param.hp = temp.hp;
        param.maxHp = temp.maxHp;
        param.attack = temp.attack;
        param.defense = temp.defense;
        param.moveSpeed = temp.moveSpeed;
    }


}
