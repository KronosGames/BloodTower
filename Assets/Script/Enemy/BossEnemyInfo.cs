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
        param = BossEnemyDatabase.GetBossEnemyData(id);
    }


}
