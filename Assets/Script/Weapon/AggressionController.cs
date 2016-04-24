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

    public WeaponParam MyWeaponParam { get; set; }

    void Start()
    {
        IsFlying = IsActive = false;
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
        if(MyWeaponParam == null)
        {
            Debug.Log("MyWeaponParam is null!");
            return;
        }

        switch (coll.tag)
        {
            case "Enemy":
                Debug.Log(coll.name + "に当たった！");
                EnemyStatusManager esm = coll.GetComponent<EnemyStatusManager>();
                if(esm != null)
                {
                    Debug.Log(coll.name + "に" + MyWeaponParam.attack + "のダメージ！");
                    esm.AddHealth(-MyWeaponParam.attack);
                }

                BossEnemyStatusManager besm = coll.GetComponent<BossEnemyStatusManager>();
                if(besm != null)
                {
                    Debug.Log(coll.name + "に" + MyWeaponParam.attack + "のダメージ！");
                    besm.AddHealth(-MyWeaponParam.attack);
                }
                break;
        }
    }

}
