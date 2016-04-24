using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour {

    // 状態管理部 ////////////////////////////////////

    /// <summary>
    /// 生きているかどうか
    /// true...生きている　false...死んでいる
    /// </summary>
    public bool IsAlive { get; private set; }

    /// <summary>
    /// 状態異常管理
    /// </summary>
    public enum PlayerStatus
    {
        Burn = 0,   // やけど、燃焼
        Poison,     // 毒
        Frozen,     // 凍結      
        BloodLoss,  // 出血

        SENTINEL    // 番兵
    }


    // 実行部 ///////////////////////////////////////

    void Start()
    {
        IsAlive = true;
        InitializeStatusArray();

        InitializeHealth();
    }

    void FixedUpdate()
    {
        CheckDead();
    }

    /// <summary>
    /// 対応した状態異常であるかどうか
    /// </summary>
    bool[] isInTheStatus = new bool[(int)PlayerStatus.SENTINEL];

    /// <summary>
    /// ステータス一覧を初期化する
    /// isInTheStatusの全要素にfalseを設定する
    /// </summary>
    public void InitializeStatusArray()
    {
        for(int index = 0;index < (int)PlayerStatus.SENTINEL;++index)
        {
            isInTheStatus[index] = false;
        }
    }

    /// <summary>
    /// 状態異常の設定をする
    /// </summary>
    /// <param name="setStatusID">設定する状態のenum</param>
    /// <param name="boolian">設定するbool型</param>
    public void SetStatus(PlayerStatus setStatusID, bool boolian = true)
    {
        int arrayIndex = (int)setStatusID;

        isInTheStatus[arrayIndex] = boolian;
    }

    /// <summary>
    /// 状態以上にあるかどうかを判定する
    /// </summary>
    /// <param name="checkStatusID">確認する状態のenum</param>
    /// <returns></returns>
    public bool IsInTheStatus(PlayerStatus checkStatusID)
    {
        return isInTheStatus[(int)checkStatusID];
    }
    
    /// <summary>
    /// 生きている状態にする
    /// </summary>
    /// <param name="health">生きている状態にした際のHP</param>
    public void Revive(int health)
    {
        IsAlive = true;
        nowHealth = health;
    }

    /// <summary>
    /// HPを確認して、0以下の場合死んでいる状態にする
    /// </summary>
    void CheckDead()
    {
        if (IsHealthUnderZero())
        {
            IsAlive = false;
        }
    }

    // HP管理部 /////////////////////////////////

    int maxHealth = int.MaxValue;

    int nowHealth = int.MaxValue;

    public void InitializeHealth()
    {
        maxHealth = GameCharacterParam.GetMaxHp();
        nowHealth = GameCharacterParam.GetHp();
    }


    /// <summary>
    /// HPに加算する
    /// </summary>
    /// <param name="addValue"></param>
    /// <returns>計算後のHP量</returns>
    public int AddHealth(int addValue)
    {
        nowHealth += addValue;


        return ClipHealth();

    }

    /// <summary>
    /// HPが0以下かどうか
    /// </summary>
    /// <returns></returns>
    bool IsHealthUnderZero()
    {

        if (nowHealth <= 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// HPを0から最大値の間に合わせる
    /// </summary>
    /// <returns>合わせた後の数値</returns>
    int ClipHealth()
    {
        nowHealth = Mathf.Min(nowHealth, maxHealth);
        nowHealth = Mathf.Max(nowHealth, 0);

        return nowHealth;
    }
}
