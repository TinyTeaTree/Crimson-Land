using UnityEngine;
using System.Collections;

public class DontDieScript : MonoBehaviour {


	void Awake () {
        DontDestroyOnLoad(gameObject);
	}
	
}
