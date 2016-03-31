/*
 * @file CameraMovementController.cs
 * @brief カメラの移動操作に関する操作を管理するスクリプト
 * @date 2016-03-28
 * @auther kondo
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// カメラの移動に関するクラス
/// </summary>
public class CameraMovementController : MonoBehaviour {

    /// <summary>
    /// 注視する対象（プレイヤー）
    /// </summary>
    [SerializeField]
    Transform target = null;

    /// <summary>
    /// 横方向のカメラの回転速度
    /// </summary>
    [Tooltip("横方向のカメラの回転速度")]
    public float rotateAnglePerSecond = 10f;
    
    /// <summary>
    /// カメラが見下げ（見上げ）ている角度
    /// 1で直上、0で真横となる。
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    float cameraUpperAngle = 0.5f;

    float cameraHorizontalAngle = 0f;
    
    /// <summary>
    /// カメラの対象への距離
    /// </summary>
    [SerializeField, Tooltip("カメラの対象への距離")]
    float cameraDistance = 10f;

    enum STATE
    {
        UnderControl,   //操作可能
        OutOfControl
    }

    STATE state = STATE.OutOfControl;

    /// <summary>
    /// 起動時にカメラをコントロール出来る状態にするかどうか
    /// </summary>
    [SerializeField]
    bool UnderControlOnAwake = true;

	// Use this for initialization
	void Start () {

        if (zoomRateArray.Length == 0)
        {
            Debug.LogError("ZoomRateを登録してください！");
        }
        else
        {
            cameraDistance = moveToZoomRate = zoomRateArray[0];

        }

        if (UnderControlOnAwake)
        {
            state = STATE.UnderControl;

        }

    }

    // Update is called once per frame
    void FixedUpdate () {
        switch (state)
        {
            case STATE.UnderControl:
                Zoom();
                RotateAroundTarget();
                transform.LookAt(target);

                break;

            case STATE.OutOfControl:
                break;

        }
	}


    /// <summary>
    /// targetを中心に回転運動
    /// </summary>
    void RotateAroundTarget()
    {

        cameraHorizontalAngle += Input.GetAxisRaw("CameraRotateHorizontal") * Time.deltaTime;
        cameraHorizontalAngle %= 1f;

        float verticalDistance = Mathf.Sin(cameraUpperAngle * Mathf.PI * 0.5f) * cameraDistance;

        float horizontalDistance = Mathf.Cos(cameraUpperAngle * Mathf.PI * 0.5f) * cameraDistance;
        float XDistance = Mathf.Cos(cameraHorizontalAngle * Mathf.PI * 2f) * horizontalDistance;
        float ZDistance = Mathf.Sin(cameraHorizontalAngle * Mathf.PI * 2f) * horizontalDistance;


        transform.position = target.position + new Vector3(XDistance, verticalDistance, ZDistance);


    }

    /// <summary>
    /// 拡大率（カメラの距離)の一覧
    /// </summary>
    [SerializeField]
    float[] zoomRateArray;

    /// <summary>
    /// するべき拡大率
    /// </summary>
    float moveToZoomRate;

    /// <summary>
    /// 現在設定されている拡大率の配列の番号
    /// </summary>
    int zoomRateIndex = 0;

    [SerializeField]
    float zoomLerpRate = 0.95f;

    void Zoom()
    {



        if (InputManager.IsDown(INPUT_ID.ZOOM))
        {
            zoomRateIndex++;

            if (zoomRateArray.Length == zoomRateIndex)
            {
                zoomRateIndex = 0;
            }
            moveToZoomRate = zoomRateArray[zoomRateIndex];
        }


        cameraDistance = Mathf.Lerp(cameraDistance, moveToZoomRate, zoomLerpRate);

    }

}
