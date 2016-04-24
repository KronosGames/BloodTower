using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

    AggressionController aggressionController = null;
	
	// Update is called once per frame
	void Update () {
	    if(InputManager.IsDown(INPUT_ID.PLAYER_ATTACK))
        {
            if(aggressionController != null)
            {
                aggressionController.ActivateWeapon(1 /*武器に応じた攻撃時間を与える*/);
            }
            else
            {
                Debug.Log("素手ぱーんち！");
                /*武器を持っていないときの攻撃*/
            }
        }
	}

    public void SetAggressionController(AggressionController aggr)
    {

        aggressionController = aggr;
    }

}
