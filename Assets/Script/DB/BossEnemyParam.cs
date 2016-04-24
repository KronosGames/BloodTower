// -----------------------------------------
// BossEnemy データID
// -----------------------------------------


public enum BOSS_ENEMY_ID
{
    NULL = -1,     //< NULL
    KONDO_MASAHIKO = 0,     //< 近藤雅彦
}

public enum BOSS_ENEMY_STATUS
{
    Burn = 0,   //< やけど、燃焼
    Poison,     //< 毒
    Frozen,     //< 凍結      
    BloodLoss,  //< 出血

    SENTINEL    //< 番兵
}

public class BossEnemyParam
{
    public BOSS_ENEMY_ID id;                                                     //< ID
    public string name;                                                          //< 名前
    public int hp;                                                               //< HP
    public int maxHp;                                                            //< HP上限
    public int attack;                                                           //< 攻撃力
    public int defense;                                                          //< 防御力
    public int moveSpeed;                                                        //< 移動速度
    public bool[] canInTheStatus = new bool[(int)BOSS_ENEMY_STATUS.SENTINEL];    //< 状態異常にかかるかどうか
    public int[] statusResistance = new int[(int)BOSS_ENEMY_STATUS.SENTINEL];    //< 状態異常耐性値(この値を超えると状態異常になる)
}
