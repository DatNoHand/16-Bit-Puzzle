using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Buttons : MonoBehaviour {


	public void NewGame() {
		Debug.Log("clicked newgame");
		// TODO
		Debug.Log("starting GameControl.ChangeScene(1)");
		GameControl.control.ChangeScene(1);
	}
	public void LoadGame() {

		GameControl.control.Load();
		GameControl.control.ChangeScene(GameControl.control.lastEnteredLevel);
	}
	public void Cheats() {
		
		GameControl.control.devMode = true;
		Debug.Log("activated devmode");
	}
	public void Resume() {
		Debug.Log("resume");
		GameControl.control.paused = false;
	}
	public void Restart() {
        
        GameControl.control.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }
	public void MainMenu() {
        
        GameControl.control.ChangeScene(0);
    }
	public void Quit() {
        
        Application.Quit();
    }
}