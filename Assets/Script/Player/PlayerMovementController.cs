/* 
 * @file PlayerMovementController.cs
 * @brief プレイヤーの移動に関する操作を管理するスクリプト
 * @date 2016-03-28
 * @auther kondo
 */


using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの移動に関するクラス
/// </summary>
public class PlayerMovementController : MonoBehaviour {


    /// <summary>
    /// プレイヤーの移動方向
    /// </summary>
    Vector3 moveVelocity = Vector3.zero;

    /// <summary>
    /// プレイヤーの移動速度
    /// </summary>
    float moveSpeed = 10f;

    /// <summary>
    /// プレイヤーの状態一覧
    /// </summary>
    enum STATE
    {
        Unactive = -1,
        Active = 1,
    }

    /// <summary>
    /// プレイヤーの状態を管理する
    /// </summary>
    STATE state = STATE.Unactive;

    [SerializeField,Tooltip("開始時にプレイヤーを操作可能かどうか")]
    bool ActiveOnAwake = true;

    /// <summary>
    /// 自分のRigidBody
    /// </summary>
    Rigidbody myRigid = null;

    /// <summary>
    /// カメラの情報を得る
    /// </summary>
    Transform cameraTransform = null;

    /// <summary>
    /// ジャンプする力
    /// </summary>
    [SerializeField]
    float jumpPower = 5f;

    [SerializeField]
    float gravityPower = 9.8f;

    /// <summary>
    /// 地面に触れているかどうか≒ジャンプが出来る状態
    /// </summary>
    bool isTouchingFloor = true;


    void Start () {

        Physics.gravity = new Vector3(0, gravityPower, 0);


        AttachComponents();
        LoadParameters();

        if (ActiveOnAwake)
        {
            state = STATE.Active;
        }
	}
	
	void FixedUpdate () {

        switch (state)
        {
            case STATE.Unactive:
                break;

            case STATE.Active:
                RefreshVelocity();
                Move();
                Jump();

                break;
        }

	}

    /// <summary>
    /// GetComponent()等あてはめ処理を行います。
    /// </summary>
    void AttachComponents()
    {
        myRigid = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

    }

    /// <summary>
    /// パラメータを読み込みあてはめます
    /// </summary>
    void LoadParameters()
    {
        moveSpeed = GameCharacterParam.GetMoveSpeed();

    }

    /// <summary>
    /// 入力等をもとにプレイヤーの移動方向を決定する
    /// </summary>
    void RefreshVelocity()
    {
        moveVelocity = cameraTransform.right * InputManager.GetAxisRaw(INPUT_ID.PLAYER_MOVE_HORIZONTAL) - cameraTransform.forward * InputManager.GetAxisRaw(INPUT_ID.PLAYER_MOVE_VERTICAL);
        moveVelocity = new Vector3(moveVelocity.x, 0, moveVelocity.z).normalized;
    }

    /// <summary>
    /// 移動させる
    /// </summary>
    void Move()
    {
        //RigidBodyを使う場合の処理
        myRigid.AddForce(moveVelocity * moveSpeed, ForceMode.VelocityChange);

    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        if (InputManager.IsDown(INPUT_ID.PLAYER_JUMP) && isTouchingFloor)
        {
            myRigid.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            isTouchingFloor = false;
        }
    }

    void OnCollisionEnter(Collision coll)
    {

        ///レイヤーやタグでやるなら変更すること
        if (coll.transform.tag == "Floor")
        {
            isTouchingFloor = true;
        }

    }

}
