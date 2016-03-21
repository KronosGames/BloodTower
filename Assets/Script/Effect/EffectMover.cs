using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour
{
    Rigidbody rigidBody = null;
    bool isHit = false;
    GameObject hitObj = null;

    // 飛ばす方向を設定
    public void SetTargetPosition(Vector3 targetPosition, float power)
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        var direction = (targetPosition - transform.position).normalized;
        rigidBody.velocity = direction * power;

        isHit = false;
        hitObj = null;
    }

    void OnTriggerEnter(Collider hit)
    {
        // Hit処理
        if (hit.tag == "Player") return;

        isHit = true;
        hitObj = hit.gameObject;
    }

    // ヒットしたかどうか
    public bool IsHit()
    {
        return isHit;
    }

    public GameObject GetHitObject()
    {
        return hitObj;
    }

}
