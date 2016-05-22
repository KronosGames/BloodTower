using UnityEngine;
using System.Collections;

public class MoveToTargetByNaviAgent : MonoBehaviour {

    [SerializeField]
    Transform target;

    [SerializeField]
    bool moveOnAwake = false;

    public bool CanMoveToTarget { get; set; }

    [SerializeField]
    float minDistance = 2f;

    NavMeshAgent navMeshAgent = null;

	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(moveOnAwake)
        {
            CanMoveToTarget = true;
            navMeshAgent.destination = target.position;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (CanMoveToTarget)
        {
            MoveToTarget();
        }


        if (Vector3.Distance(transform.position, target.position) < minDistance)
        {
            CanMoveToTarget = false;
            navMeshAgent.Stop();
        }
        else
        {
            CanMoveToTarget = true;
            navMeshAgent.Resume();
        }

	}

    void MoveToTarget()
    {
        navMeshAgent.destination = target.position;
    }
}
