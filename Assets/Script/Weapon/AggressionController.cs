using UnityEngine;
using System.Collections;


/// <summary>
/// 武器のあたり判定（攻撃判定）を有効にする
/// </summary>
public class AggressionController : MonoBehaviour {

    /// <summary>
    /// 有効な時間
    /// </summary>
    float activeTime = 0f;

    /// <summary>
    /// 有効な状態かどうか
    /// </summary>
    public bool IsActive;

    /// <summary>
    /// 飛んでいるかどうか（武器が投げられている）
    /// まだ飛んでいるときの処理は未定
    /// </summary>
    public bool IsFlying { get; set; }

    CapsuleCollider myCollider = null;

    void Start()
    {
        IsFlying = IsActive = false;
        myCollider = GetComponent<CapsuleCollider>();
    }

    /// <summary>
    /// 武器の攻撃能力を有効化する
    /// </summary>
    /// <param name="_time">有効にする時間</param>
    public void ActivateWeapon(float _time)
    {
        if(_time <= 0)
        {
            Debug.Log("設定する時間が短いですぞ");
        }

        activeTime = _time;
        IsActive = true;


    }

    void FixedUpdate()
    {
        CountTime();
    }

    void CountTime()
    {
        if (IsActive && activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }

        if (activeTime <= 0)
        {
            IsActive = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!IsActive) return;

        if(IsFlying)
        {
            FlyingCollEvents(ref coll);
        }
        else
        {
            SwingCollEvents(ref coll);
        }

    }

    /// <summary>
    /// 武器が飛んでる時の当たった処理判定一覧
    /// </summary>
    void FlyingCollEvents(ref Collider coll)
    {
        /// まだ飛んでいるときの処理は未定

    }

    /// <summary>
    /// 武器を振っている時の当たった処理判定一覧
    /// </summary>
    void SwingCollEvents(ref Collider coll)
    {
        switch (coll.tag)
        {
            case "Enemy":
                Debug.Log("Enemyに当たった！");
                break;
        }
    }

}
