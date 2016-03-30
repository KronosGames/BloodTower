using UnityEngine;
using System.Collections;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    ENEMY_ID id = ENEMY_ID.NULL;

    EnemyParam param = null;

    //  ----------------------------------------------
    //  公開用 関数
    //  ----------------------------------------------

    public EnemyParam GetParam() { return param; }

    public void Setup()
    {
        EnemyParam temp = EnemyDatabase.GetEnemyParam(id);

        param = new EnemyParam();
        param.id = temp.id;
        param.name = temp.name;
        param.hp = temp.hp;
        param.maxHp = temp.maxHp;
        param.attack = temp.attack;
        param.defense = temp.defense;
        param.moveSpeed = temp.moveSpeed;
    }



}
