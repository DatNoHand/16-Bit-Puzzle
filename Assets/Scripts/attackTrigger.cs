using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    public attackTrigger damageHandler;

    public int dmg = 20;
    private Player player;

    void Start (){
    	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update(){
        if (GameControl.control.score > 0 && GameControl.control.score <= player.pointsForLevel1){
            GameControl.control.level = 0;
            dmg = 30;
        }
        else if (GameControl.control.score > player.pointsForLevel1 && GameControl.control.score <= player.pointsForLevel2){
            GameControl.control.level = 1;
            dmg = 40;
        }
        else if (GameControl.control.score > player.pointsForLevel2 && GameControl.control.score <= player.pointsForLevel3){
            GameControl.control.level = 2;
            dmg = 50;
        }
        else if (GameControl.control.score > player.pointsForLevel3){
            GameControl.control.level = 3;
            dmg = 60;
        }
        else {
            GameControl.control.level = 0;
        }
        
        GameControl.control.dmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.isTrigger && col.CompareTag("enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
