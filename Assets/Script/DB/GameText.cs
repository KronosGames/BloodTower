// -----------------------------------------
// GameText データID
// -----------------------------------------

public enum GAMETEXT_ID
{
    NULL = 0,     //< NULL
    GAME_TITLE = 1,     //< ゲームタイトル
    TITLE_START = 2,     //< タイトル時のスタートテキスト
}

public class GameText
{
    public GAMETEXT_ID id;
    public string text;
}
