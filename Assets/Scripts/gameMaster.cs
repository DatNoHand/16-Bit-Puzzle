using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {
	
	public int points;
	public Text pointsText;
	public int scoreLength = 9;

	void Update(){

		pointsText.text = ""+points.ToString("00000000");
	}
	
}
