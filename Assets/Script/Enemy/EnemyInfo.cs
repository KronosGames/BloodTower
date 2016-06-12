using UnityEngine;
using System.Collections;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    ENEMY_ID id = ENEMY_ID.NULL;

    [SerializeField]
    bool isBoss = false;

    EnemyParam param = null;
    EnemyStatusManager statusManager = null;
	int hierarchyCount = -1;

    //  ----------------------------------------------
    //  公開用 関数
    //  ----------------------------------------------

    public EnemyParam GetParam() { return param; }
    public bool IsBoss() { return isBoss; }
	public int GetHierarchyCount() { return hierarchyCount; }

    public void Setup(int hierarchyCount)
    {
		this.hierarchyCount = hierarchyCount;

		EnemyParam temp = EnemyDatabase.GetEnemyParam(id);

        param = new EnemyParam();
        param.id = temp.id;
        param.name = temp.name;
        param.hp = temp.hp;
        param.maxHp = temp.maxHp;
        param.attack = temp.attack;
        param.defense = temp.defense;
        param.moveSpeed = temp.moveSpeed;

        // 状態異常の初期化
        for (int index = 0; index < (int)ENEMY_STATUS.SENTINEL; ++index)
        {
            param.canInTheStatus[index] = temp.canInTheStatus[index];
            param.statusResistance[index] = temp.statusResistance[index];
        }

        statusManager = GetComponent<EnemyStatusManager>();
        if (statusManager == null)
        {
            Debug.LogError("EnemyStatusManager is NULL ");
        }

        statusManager.Setup(this);
    }


}
