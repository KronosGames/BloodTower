// -----------------------------------------
// BossEnemy データID
// -----------------------------------------


public enum BOSS_ENEMY_ID
{
    NULL = -1,     //< NULL
    KONDO_MASAHIKO = 0,     //< 近藤雅彦
}

public class BossEnemyParam
{
    public BOSS_ENEMY_ID id;
    public string name;
    public int hp;
    public int maxHp;
    public int attack;
    public int defense;
    public int moveSpeed;
}
