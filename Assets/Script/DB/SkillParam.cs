// -----------------------------------------
// Skill データID
// -----------------------------------------

public enum SKILL_ID
{
    CREATE_FIRE = 1,     //< 炎を出す
    POWER_UP = 0,     //< 攻撃力増加
    NULL = -1,     //< NULL
}

public class SkillParam
{
    public SKILL_ID id;
    public string name;
    public string explain;
}
