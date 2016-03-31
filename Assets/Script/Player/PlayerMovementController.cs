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
    /// ダッシュ時の移動速度倍率
    /// </summary>
    [SerializeField, Tooltip("ダッシュ時の移動速度の倍率")]
    float sprintRate = 1.5f;

    /// <summary>
    /// 空中での移動速度
    /// </summary>
    [SerializeField, Tooltip("空中での移動速度")]
    float FallingMoveRate = 0.5f;

    /// <summary>
    /// プレイヤーの状態一覧
    /// </summary>
    enum STATE
    {
        Unactive = -1,
        Active = 1,
    }

    enum MOVE_TYPE
    {
        IDOLING,
        WALKING,
        ROLLING,
        JUMPING,
    }

    MOVE_TYPE moveType = MOVE_TYPE.IDOLING;


    /// <summary>
    /// プレイヤーの状態を管理する
    /// </summary>
    STATE state = STATE.Unactive;

    [SerializeField, Tooltip("開始時にプレイヤーを操作可能かどうか")]
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

    /// <summary>
    /// ローリングの速度
    /// </summary>
    [SerializeField]
    float rollingSpeed = 3f;

    /// <summary>
    /// 水平方向の移動入力値
    /// </summary>
    float horizontalInput = 0;

    /// <summary>
    /// 垂直方向の移動入力値
    /// </summary>
    float verticalInput = 0;


    void Start() {

        Physics.gravity = new Vector3(0, gravityPower, 0);


        AttachComponents();
        LoadParameters();

        if (ActiveOnAwake)
        {
            state = STATE.Active;
        }
    }

    /// <summary>
    /// 入力の更新
    /// </summary>
    void RefreshInputValues()
    {
        horizontalInput = InputManager.GetAxisRaw(INPUT_ID.PLAYER_MOVE_HORIZONTAL);
        verticalInput = InputManager.GetAxisRaw(INPUT_ID.PLAYER_MOVE_VERTICAL);

        if (moveType != MOVE_TYPE.ROLLING && moveType != MOVE_TYPE.JUMPING)
        {
            if (horizontalInput == 0 && verticalInput == 0)
            {
                moveType = MOVE_TYPE.IDOLING;

                if (InputManager.IsUp(INPUT_ID.PLAYER_ROLLING))
                {
                    moveType = MOVE_TYPE.ROLLING;
                    moveVelocity = transform.forward;
                }

            }
            else
            {
                RefreshVelocity();
                moveType = MOVE_TYPE.WALKING;

                if (InputManager.IsUp(INPUT_ID.PLAYER_ROLLING))
                {
                    moveType = MOVE_TYPE.ROLLING;
                    myRigid.AddForce(moveVelocity * rollingSpeed, ForceMode.VelocityChange);
                }

            }

        }

    }

    void FixedUpdate()
    {
        if (state == STATE.Unactive) return;

        RefreshInputValues();

        switch (moveType)
        {
            case MOVE_TYPE.IDOLING:
                Jump();

                break;

            case MOVE_TYPE.WALKING:

                if (InputManager.IsPress(INPUT_ID.PLAYER_SPRINT))
                {
                    Move(moveSpeed * sprintRate);
                }
                else
                {
                    Move(moveSpeed);
                }

                Jump();

                break;

            case MOVE_TYPE.ROLLING:
                Rolling();
                break;

            case MOVE_TYPE.JUMPING:

                Move(moveSpeed * FallingMoveRate);

                if (isTouchingFloor)
                {
                    moveType = MOVE_TYPE.IDOLING;
                }
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
        moveVelocity = cameraTransform.right * horizontalInput - cameraTransform.forward * verticalInput;
        moveVelocity = new Vector3(moveVelocity.x, 0, moveVelocity.z).normalized;
    }

    /// <summary>
    /// 移動させる
    /// </summary>
    void Move(float _speed)
    {
        myRigid.AddForce(moveVelocity * _speed, ForceMode.VelocityChange);

    }


    /// <summary>
    /// ローリング１回の時間
    /// </summary>
    [SerializeField]
    float rollingInterval = 1f;

    /// <summary>
    /// ローリング開始してからの時間
    /// </summary>
    float rollingTime = 0f;

    /// <summary>
    /// ローリング
    /// </summary>
    void Rolling()
    {
        myRigid.AddForce(moveVelocity * rollingSpeed, ForceMode.VelocityChange);


        rollingTime += Time.deltaTime;

        if (rollingTime >= rollingInterval)
        {
            rollingTime = 0;
            moveType = MOVE_TYPE.IDOLING;
        }
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
            moveType = MOVE_TYPE.JUMPING;
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
