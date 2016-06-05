using UnityEngine;
using System.Collections;

public enum RIG_ID
{
    NULL = -1,
    BACK_HIP,    //< 背中 腰
    FORWARD_HIP,    //< 腹 腰

	RIGHT_KNEE,     //< 右膝
	LEFT_KNEE,		//< 左膝

	RIGHT_SOLE,  //< 右 足裏
	LEFT_SOLE,  //< 左 足裏

	RIGHT_HAND,	//< 右手
	LEFT_HAND,  //< 左手

	RIGHT_SHOULDER,		//< 右肩
	LEFT_SHOULDER,      //< 左肩

	RIGHT_ELBOW,		//< 右肘	
	LEFT_ELBOW,         //< 左肘

	BREAST,		//< 胸
	HEAD,		//< 頭

	MAX,
}

[System.Serializable]
public class RigRegisterData
{
	public RIG_ID id = RIG_ID.NULL;
	public Transform trans = null;
}

public class CharacterRigRegister : MonoBehaviour
{
	[SerializeField]
	RigRegisterData[] rigsterDataList = new RigRegisterData[(int)RIG_ID.MAX];

	// RigのTransformを取得します。
	// 取得するのは、マイフレームはやめてください。
	public Transform GetRigTransform(RIG_ID id)
	{
		for (int i = 0; i < rigsterDataList.Length; i++)
		{
			if (rigsterDataList[i].id == id)
			{
				return rigsterDataList[i].trans;
			}
		}
		return null;
	}

}
