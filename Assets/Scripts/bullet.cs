using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public int bulletDamage = 1;
    public int invincibilityTime = 1;
    public float bulletTimer;
    private Player player;

    // Knockback Stats
    public float knockDur = 0.002f;
    public float knockPwr = 500;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
         if (col.CompareTag("player_hitbox"))
         {
            player.Damage(bulletDamage, invincibilityTime);
            Destroy(gameObject);
         }
    }
    private void FixedUpdate()
    {
        if (bulletTimer < 2)
        {
            bulletTimer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
