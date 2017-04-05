using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class mainMenuOptions : MonoBehaviour {

public void NewGame() {

		// TODOy
    	StartCoroutine(GameControl.ChangeScene(1));
    }
public void LoadGame() {

		GameControl.control.Load();
		StartCoroutine(GameControl.ChangeScene(GameControl.control.lastEnteredLevel));
	}
public void Cheats() {
		
		GameControl.control.devMode = true;
	}
public void Resume() {
		
		GameControl.control.paused = false;
	}
public void Restart() {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
public void MainMenu() {
        
        StartCoroutine(GameControl.ChangeScene(0));
    }
public void Quit() {
        
        Application.Quit();
    }
}