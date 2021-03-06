﻿using UnityEngine;
using System.Collections;

public class RandomIntensity : MonoBehaviour {


    [SerializeField]
    float minIntensity = 0.0f;

    [SerializeField]
    float maxIntensity = 5.0f;

    [SerializeField]
    float changeInterval = 0.5f;

    float changedCoolTime = 0f;

    [SerializeField]
    float lerpRate = 0.95f;

    Light thisLight = null;

    /// <summary>
    /// 変更先に光の強さ
    /// </summary>
    float changeToIntensity = 0f;


    void Start()
    {
        thisLight = GetComponent<Light>();
    }

	// Update is called once per frame
	void FixedUpdate () {

        changedCoolTime += Time.deltaTime;

	    if(changedCoolTime >= changeInterval)
        {
            changeToIntensity = Random.Range(minIntensity, maxIntensity);
            changedCoolTime = 0f;
        }

        thisLight.intensity = Mathf.Lerp(thisLight.intensity, changeToIntensity, lerpRate);

    }
}
