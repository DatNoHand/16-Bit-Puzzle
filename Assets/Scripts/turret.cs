using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {

    // Integers
    public int curHealth;
    public int maxHealth = 60;

    // Floats
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    // Booleans
    public bool awake = false;
    public bool lookingRight;

    // References
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft, shootPointRight;

    void Awake()
    {
        // TODO
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
    }

    private void Update()
    {
        anim.SetBool("awake", awake);
        anim.SetBool("lookingRight", lookingRight);

        RangeCheck();

        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }
        else
        {
            lookingRight = true;
        }
        
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakeRange)
        {
            awake = true;
        }
        if (distance > wakeRange)
        {
            awake = false;
        }

    }
    public void Attack(bool attackingLeft)
    {
        bulletTimer += Time.deltaTime;

        if ( bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            if (attackingLeft)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }

            if (!attackingLeft)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }

        }
    }
    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("p_damage");
    }
    
}
