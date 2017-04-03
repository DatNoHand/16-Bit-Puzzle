using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    public int dmg = 20;
    private gameMaster gm;
    private Player player;

    void Start (){
    	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    	gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    void Update(){
        if (gm.points > 0 && gm.points <= player.pointsForLevel1){
            player.level = 0;
            dmg = 30;
        }
        else if (gm.points > player.pointsForLevel1 && gm.points <= player.pointsForLevel2){
            player.level = 1;
            dmg = 40;
        }
        else if (gm.points > player.pointsForLevel2 && gm.points <= player.pointsForLevel3){
            player.level = 2;
            dmg = 50;
        }
        else if (gm.points > player.pointsForLevel3){
            player.level = 3;
            dmg = 60;
        }
        else {
            player.level = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.isTrigger && col.CompareTag("enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
