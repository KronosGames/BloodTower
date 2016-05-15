using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

    AggressionController aggressionController = null;

    [SerializeField]
    PlayerAnimationController playerAnimationController = null;
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(InputManager.IsDown(INPUT_ID.PLAYER_ATTACK))
        {
            if(aggressionController != null)
            {
                aggressionController.ActivateWeapon(1 /*武器に応じた攻撃時間を与える*/);
                playerAnimationController.BeginAttack(1, 1.3f/*武器に応じた攻撃時間を与える*/);
            }
            else
            {
                Debug.Log("素手ぱーんち！");
                /*武器を持っていないときの攻撃*/
                playerAnimationController.BeginAttack(0, 1.0f);
            }
        }
	}

    public void SetAggressionController(AggressionController aggr)
    {

        aggressionController = aggr;
    }

}
