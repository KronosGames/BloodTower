// -----------------------------------------
// Enemy データID
// -----------------------------------------


public enum ENEMY_ID
{
    NULL = -1,     //< NULL
    VANTAN = 0,     //< バンタン
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
}
