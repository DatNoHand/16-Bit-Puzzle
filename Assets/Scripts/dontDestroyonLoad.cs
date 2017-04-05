using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyonLoad : MonoBehaviour {

	public static dontDestroyonLoad dontdestroy;

	void Awake () {

		DontDestroyOnLoad(this);
	}
}
