using UnityEngine;
using System.Collections;

/// <summary>
/// 指定した範囲に狙ったものがいるかどうかを判断し返す関数群を提供する
/// 対応するクラスを引数により呼び出す関数を判別します。
/// </summary>
public class CollisionChecker2D
{
    /// <summary>
    /// 指定した点からの円のあたり判定を行う際の情報
    /// </summary>
    public class CircleAreaData
    {
        public CircleAreaData(Vector3 center,float rad)
        {
            centerPosition = center;
            radius = rad;
        }
        public Vector3 centerPosition;
        public float radius;
    }

    /// <summary>
    /// 指定した点からの扇形のあたり判定を行う際の情報
    /// ～～～EularAngleは、X+を0かつ360、X-を180、Z+を90、Z-を270とする。
    /// beginEularAngleは反時計回りに数値が増えることを想定しています。
    /// ※もしGameObjectの回転方向を与えたい場合はbeginRadianにY軸回転を加算すればOK
    /// </summary>
    public class FanAreaData
    {
        public FanAreaData(Vector3 center,float minRange,float maxRange,float beginEularAngle,float arcAngle)
        {
            this.centerPosition = center;
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.beginEularAngle = beginEularAngle;
            this.arcAngle = arcAngle;
        }

        public Vector3 centerPosition;
        public float minRange;
        public float maxRange;
        public float beginEularAngle;
        public float arcAngle;
    };

    // 円との当たり判定
    public static bool IsInside(Vector3 targetPosition, CircleAreaData circleArea)
    {
        // 距離の判定を行う
        if (Vector2.Distance(new Vector2(targetPosition.x, targetPosition.z), new Vector2(circleArea.centerPosition.x, circleArea.centerPosition.z)) > circleArea.radius)
        {
            return false;
        }

        return true;
    }

    // 扇型との当たり判定
    public static bool IsInside(Vector3 targetPosition,FanAreaData fanArea)
    {
        
        float dx = targetPosition.x - fanArea.centerPosition.x;
        float dy = targetPosition.z - fanArea.centerPosition.z;

        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        if (distance < fanArea.minRange || distance > fanArea.maxRange)
        {
            return false;
        }

        fanArea.beginEularAngle = Mathf.Deg2Rad * (fanArea.beginEularAngle - 90f);
        float endEularAngle   = Mathf.Deg2Rad * (fanArea.beginEularAngle + fanArea.arcAngle - 90f);

        float sx = (float)Mathf.Cos(fanArea.beginEularAngle);
        float sy = (float)Mathf.Sin(fanArea.beginEularAngle);
        float ex = (float)Mathf.Cos(endEularAngle);
        float ey = (float)Mathf.Sin(endEularAngle);

        if (sx * ey - ex * sy > 0)
        {
            if (sx * dy - dx * sy < 0) return false; // second test
            if (ex * dy - dx * ey > 0) return false; // third test
            return true; // all test passed
        }
        else
        {
            if (sx * dy - dx * sy > 0) return true; // second test
            if (ex * dy - dx * ey < 0) return true; // third test
            return false; // all test failed
        }


    }

}
