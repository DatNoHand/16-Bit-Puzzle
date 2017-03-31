using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour {

    private Player player;
    public int spikeDamage = 1;
    public int invincibilityTime = 1;

    // Knockback Stats
    public float knockDur = 0.002f;
    public float knockPwr = 150;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player_hitbox"))
        {
            player.Damage(spikeDamage, invincibilityTime);
            player.Knockback(knockDur, knockPwr, player.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("player_hitbox"))
        {
            player.Damage(spikeDamage, invincibilityTime);
            player.Knockback(knockDur, knockPwr, player.transform.position);
        }
    }
}