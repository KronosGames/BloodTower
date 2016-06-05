using UnityEngine;
using System.Collections;

public class MoveToTargetByNaviAgent : MonoBehaviour {

    [SerializeField]
    Transform target = null;

    [SerializeField]
    bool moveOnAwake = false;

    public bool CanMoveToTarget { get; set; }

    NavMeshAgent navMeshAgent = null;

	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (moveOnAwake)
        {
            CanMoveToTarget = true;
            navMeshAgent.destination = target.position;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (CanMoveToTarget)
        {
            // NavMeshAgentのStoppingDistanceにて設定することで、
            // 接近を停止する距離を設定できる。
            MoveToTarget();
        }
	}

    void MoveToTarget()
    {
        navMeshAgent.destination = target.position;
    }
}
