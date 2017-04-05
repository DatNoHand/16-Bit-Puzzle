using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class mainMenuOptions : MonoBehaviour {

public void newGame() {

		// TODO
    	StartCoroutine(GameControl.ChangeScene(1));
    }
public void loadGame() {

		GameControl.control.Load();
		StartCoroutine(GameControl.ChangeScene(GameControl.control.lastEnteredLevel));
	}
public void cheats() {

		GameControl.control.devMode = true;
}

}