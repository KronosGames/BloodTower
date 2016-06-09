using UnityEngine;
using System.Collections;

public class WeaponCatchReleaseController : MonoBehaviour {

    /// <summary>
    /// 拾える範囲
    /// </summary>
    [SerializeField]
    float catchableArea = 0.5f;

    /// <summary>
    /// 自分のTransform
    /// </summary>
    Transform myTransform;

    /// <summary>
    /// 装備している武器
    /// </summary>
    GameObject equipedWeapon = null;

    /// <summary>
    /// 装備している状態かどうか
    /// </summary>
    public bool IsEquiped { get; set; }

    /// <summary>
    /// 拾った武器の過去の親
    /// </summary>
    Transform weaponPrevParent = null;

    [SerializeField]
    float inputThreshold = 0.3f;

	[SerializeField]
	CharacterRigRegister characterRig = null;

	PlayerAttackController attackController = null;

    // Use this for initialization
    void Start () {
        myTransform = transform;
        attackController = GetComponent<PlayerAttackController>();

    }

	// Update is called once per frame
	void Update () {
    	if(equipedWeapon == null)
        {
            if(InputManager.GetAxisRaw(INPUT_ID.PLAYER_GET_WEAPON) < -inputThreshold)
            {
                CatchWeapon();
            }
        }
        else
        {
            if(InputManager.GetAxisRaw(INPUT_ID.PLAYER_GET_WEAPON) > inputThreshold)
            {
                ReleaseWeapon();
            }
        }


	}
    
    /// <summary>
    /// 武器を拾う
    /// </summary>
    void CatchWeapon()
    {
        equipedWeapon = GetCatchableGameObject();
        if (equipedWeapon != null)
        {
            equipedWeapon.GetComponent<Rigidbody>().isKinematic = true;

            IsEquiped = true;

            AggressionController ac = equipedWeapon.GetComponent<AggressionController>();
            if(ac != null)
            {
                WeaponInfo wi = ac.GetComponent<WeaponInfo>();
                if(wi != null)
                {
                    ac.MyWeaponParam = wi.GetParam();
                    attackController.SetAggressionController(ac);
                }

            }

			Transform weaponTransform = characterRig.GetRigTransform(RIG_ID.RIGHT_HAND);
			equipedWeapon.transform.position = weaponTransform.position;
            weaponPrevParent = equipedWeapon.transform.parent;
            equipedWeapon.transform.SetParent(weaponTransform);
            equipedWeapon.transform.localRotation = Quaternion.identity;
        }


    }

    /// <summary>
    /// 拾える武器を取得する
    /// </summary>
    /// <returns>拾える距離にある場合...拾える武器 拾えない距離にある場合...null</returns>
    GameObject GetCatchableGameObject()
    {
        NearstWeaponData nearestWeapon =  BattleMapUtility.GetNearstWeapon(ref myTransform);

        if (nearestWeapon.distance <= catchableArea)
        {
            GameObject weaponObj = nearestWeapon.weaponInfo.gameObject;
            weaponObj.transform.rotation = Quaternion.identity;
            return weaponObj;
        }

        return null;
    }

    /// <summary>
    /// 武器を捨てる
    /// </summary>
    void ReleaseWeapon()
    {
        //! TODO:プレイヤーのいる階層をもとに親を決める必要がある。
        equipedWeapon.transform.SetParent(weaponPrevParent);
        ThroughWeapon();
        equipedWeapon = null;
        IsEquiped = false;
        attackController.SetAggressionController(null);
    }
    
    [SerializeField]
    float throwPower = 5f;

    Vector3 throwVelocity = Vector3.zero;

    void ThroughWeapon()
    {
        Rigidbody weaponRig = equipedWeapon.GetComponent<Rigidbody>();

        float horizonInput = InputManager.GetAxisRaw(INPUT_ID.PLAYER_MOVE_HORIZONTAL);
        float verticalInput = InputManager.GetAxisRaw(INPUT_ID.PLAYER_MOVE_VERTICAL);

        if (weaponRig != null)
        {
            weaponRig.isKinematic = false;


            if (horizonInput == 0 && verticalInput == 0)
            {
                weaponRig.AddForce(Vector3.up , ForceMode.VelocityChange);
            }
            else
            {
                throwVelocity = Camera.main.transform.right * horizonInput - Camera.main.transform.forward * verticalInput;
                throwVelocity = new Vector3(throwVelocity.x, 0, throwVelocity.z).normalized;
                weaponRig.AddForce(throwVelocity * throwPower, ForceMode.VelocityChange);
            }
        }
    }



}
