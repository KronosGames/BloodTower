using UnityEngine;
using System.Collections;

public class DontGameObjectDestroy : MonoBehaviour {

	void Awake () 
    {
        DontDestroyOnLoad(gameObject);
	}
}
