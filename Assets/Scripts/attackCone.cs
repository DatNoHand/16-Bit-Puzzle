using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackCone : MonoBehaviour {

    public turret turret;
    public bool isLeft = true;


    private void Awake()
    {
        turret = gameObject.GetComponentInParent<turret>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("player_hitbox"))
        {
                turret.Attack(isLeft);          
        }
    }

}
