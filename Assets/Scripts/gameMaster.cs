using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {
	
	public int points;
	public Text pointsText;
	public Text levelText;
	public int scoreLength = 9;
	private Player player;

	void Start (){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	void Update(){

		pointsText.text = ""+points.ToString("00000000");
		levelText.text = "Level "+ player.level;
	}
	
}
