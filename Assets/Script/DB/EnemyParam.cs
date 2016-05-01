// -----------------------------------------
// Enemy データID
// -----------------------------------------


public enum ENEMY_ID
{
    NULL = -1,     //< NULL
    VANTAN = 0,     //< バンタン
    KONDO_MASASHIKO = 1,     //< 近藤雅彦
}

public enum ENEMY_STATUS
{
    Burn = 0,   //< やけど、燃焼
    Poison = 1,   //< 毒
    Frozen = 2,   //< 凍結
    BloodLoss = 3,   //< 出血
    SENTINEL = 4,   //< 番兵
}

public class EnemyParam
{
    public ENEMY_ID id;
    public string name;
    public int hp;
    public int maxHp;
    public int attack;
    public int defense;
    public int moveSpeed;
    public bool[] canInTheStatus = new bool[(int)ENEMY_STATUS.SENTINEL];   //< 状態異常にかかるかどうか
    public int[] statusResistance = new int[(int)ENEMY_STATUS.SENTINEL];   //< 状態異常耐性値(この値を超えると状態異常になる)
}
