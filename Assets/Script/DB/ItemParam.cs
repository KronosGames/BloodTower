// -----------------------------------------
// Item データID
// -----------------------------------------

public enum ITEM_ID
{
    NONE = -1,     //< NONE
    GOLD_STONE = 0,     //< 金の石
	SILVER_STONE = 1,     //< 銀の石
    COPPER_STONE = 2,     //< 銅の石
}

public class ItemParam
{
	public ITEM_ID id;
	public string name;
	public string explain;
}