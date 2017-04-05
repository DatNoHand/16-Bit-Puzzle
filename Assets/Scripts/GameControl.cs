using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour {

	public static GameControl control;
	
	private Player player;

	// HUD
	public int score;
	public Text scoreText;
	public Text levelText;
	public Image hearts;
	public Sprite[] heartSprites;
	public Image heartUI;
	public bool paused;

	public GameObject hud;
	public GameObject ui;
	public GameObject startUI;

	// Stats
	public int curHealth;
	public int level;
	public int lastEnteredLevel;
	public int dmg;

	// Balance stuff
	public int coinWorth = 25;

	// Debug/Cheats
	public bool devMode = false;
    public bool hasInfiniteLives = false;
    public bool invincible = false;
    public bool canJumpInfinitely = false;

	public void Awake () {

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		scoreText = GameObject.Find("scoreText").GetComponent<Text>();
		levelText = GameObject.Find("levelText").GetComponent<Text>();

		hud = GameObject.Find("HUD");
		ui = GameObject.Find("UI");
		startUI = GameObject.Find("startUI");

		// Making sure only one GameControl object exists
		if (control == null) {

			DontDestroyOnLoad(gameObject);
			control = this;
		}

		else if (control != this) {

			Destroy(gameObject);
		}
	}

	void Update(){

		// Setting scoreText to score and levelText to level
		scoreText.text = ""+score.ToString("00000000");
		levelText.text = "Level "+ level;

		// Setting the Hearts to the current health
		heartUI.sprite = heartSprites[GameControl.control.curHealth];

		// If the active scene is 0, enable the startUI Object and disable the "normal" UI
		if (SceneManager.GetActiveScene().buildIndex == 0) {
		
			hud.SetActive(false);
			ui.SetActive(false);
			startUI.SetActive(true);

		}
		else {

			// If the active scene is not 0, disable startUI and enable "normal" player UI
			hud.SetActive(true);
			ui.SetActive(true);
			startUI.SetActive(false);
		}
	}

	void OnGUI () {

		// Debug Information if debug = true
		if (devMode) {

			GUI.Label(new Rect(10,10,100,30), "Health: " + curHealth);
			GUI.Label(new Rect(10,30,100,30), "Level: " + level);
			GUI.Label(new Rect(10,50,200,30), "Current Scene: "+SceneManager.GetActiveScene().name+" "+SceneManager.GetActiveScene().buildIndex);
			GUI.Label(new Rect(10,70,100,30), "Damage: "+ GameControl.control.dmg);
		}
	}

	public static IEnumerator ChangeScene(int sceneIndex) {

		float fadeTime = GameObject.Find("GameControl").GetComponent<Fading>().BeginFade(1, 0.5f);
        yield return new WaitForSeconds(fadeTime);
        Debug.Log("Waited for "+fadeTime+" seconds");
        SceneManager.LoadScene(sceneIndex);

        yield return 0;
	}

	public void Save() {

		string savelocation = Application.persistentDataPath+"/playerInfo.sav";

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savelocation);

		PlayerData data = new PlayerData();


    	// Setting variables to Save
    	data.level = level;
    	data.score = score;
    	data.lastEnteredLevel = player.lastEnteredLevel;

		

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load() {

		if (File.Exists(Application.persistentDataPath + "/playerInfo.sav")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.sav", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			// Getting variables to load
			level = data.level;
			score = data.score;
			lastEnteredLevel = data.lastEnteredLevel;
		
		}
	}
}

[Serializable]
class PlayerData {

    // Stats
    public int level;
    public int score;
    public int lastEnteredLevel;
}