using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    private bool attacking = false;
    private Player player;

    private float attackTimer = 0f;
    public float attackCooldown = 0f;
    public float attackDuration = 0.3f;
    public float attackCooldownDuration = 0.35f;

    public Collider2D attackTrigger;
    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.hasControl)
        {
            if (Input.GetButtonDown("Meele") && player.canAttack)
            {
                attacking = true;
                attackTimer = attackDuration;
                attackCooldown = attackCooldownDuration;
                attackTrigger.enabled = true;
            }

            if (attacking)
            {

                if (attackTimer > 0)
                {
                    player.canAttack = false;
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    attackTrigger.enabled = false;
                }
            }

            if (attackCooldown > 0)
                {
                    attackCooldown -= Time.deltaTime;
                }
                else
                {
                    player.canAttack = true;
                }

            anim.SetBool("attacking", attacking);
        }
    }
}
