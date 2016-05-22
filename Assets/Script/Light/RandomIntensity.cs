using UnityEngine;
using System.Collections;

public class RandomIntensity : MonoBehaviour {


    [SerializeField]
    float minIntensity = 0.0f;

    [SerializeField]
    float maxIntensity = 5.0f;

    [SerializeField]
    float changeInterval = 0.5f;

    float changedCoolTime = 0f;


    Light thisLight = null;

    void Start()
    {
        thisLight = GetComponent<Light>();
    }

	// Update is called once per frame
	void FixedUpdate () {

        changedCoolTime += Time.deltaTime;

	    if(changedCoolTime >= changeInterval)
        {
            thisLight.intensity = Random.Range(minIntensity, maxIntensity);
            changedCoolTime = 0f;
        }
	}
}
