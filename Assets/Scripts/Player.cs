using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    // Cheats
    public bool hasInfiniteLives = false;
    public bool invincible = false;
    public bool canJumpInfinitely = false;

    // Floats
    public float maxSpeed = 4f;
    public float speed = 50f;
    public float jmpPwr = 200f;
    
    // Booleans
    public bool onGround;
    public bool canDoubleJump = true;
    public bool hasControl = false;
    public bool canTakeDamage = true;
    public bool canAttack = true;
    
    public bool gotHit = false;
    

    // Vectors
    public Vector3 easeVelocity;


    // Stats
    public int curHealth;
    public int maxHealth = 5;
    public int coinWorth = 25;
    public int level = 0;

    // Level up criteria
    public int pointsForLevel1 = 50;
    public int pointsForLevel2 = 100;
    public int pointsForLevel3 = 150;
    public int pointsForLevel4 = 200;

    // References
    private Rigidbody2D rb2d;
    private Animator anim;
    private gameMaster gm;

	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

        // Healing the Player
        curHealth = maxHealth;
	}
	
	void Update () {

        anim.SetBool("onGround", onGround);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("canTakeDamage", canTakeDamage);
        anim.SetBool("gotHit", gotHit);

        // Moving the Player
        if (hasControl)
        {
            float h = Input.GetAxis("Horizontal");
            rb2d.AddForce((Vector2.right * speed) * h);

            // Flipping the Player Sprite

        	if (Input.GetAxis("Horizontal") < -0.1f)
        	{
        	    transform.localScale = new Vector3(-1, 1, 1);
        	}
        	if (Input.GetAxis("Horizontal") > 0.1f)
        	{
        	    transform.localScale = new Vector3(1, 1, 1);
        	}

            // Jump Function

            if (Input.GetButtonDown("Jump"))
            {
                if (canJumpInfinitely)
                {
                    canDoubleJump = false;
                    rb2d.AddForce(Vector2.up * jmpPwr);
                }
                else if (onGround)
                {
                    rb2d.AddForce(Vector2.up * jmpPwr);
                    canDoubleJump = true;
                }
                else if (canDoubleJump)
                    {
                        canDoubleJump = false;
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                        rb2d.AddForce(Vector2.up * jmpPwr / 1.0f);
                    }
            }
        }

        switch (level){
        	case 0: break;
        	case 1: break;
        	case 2: break;
        	case 3: break;
        	case 4: break;
        }

        // Health Management
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        // Limiting the Speed of the Player

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        

    }

    void Die()
    {
        // Load the Active Level again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Damage Function with optional Invincibility
    public void Damage(int dmg, int afterHitTime)
    {
        if (invincible) { return; }
        if (canTakeDamage)
        {
            if (curHealth < dmg)
            {
                curHealth = 0;
                Die();
                return;
            }
            if (hasInfiniteLives)
            {
                dmg = 0;
            }
            gotHit = true;
            curHealth -= dmg;
            canTakeDamage = false;

            gameObject.GetComponent<Animation>().Play("p_damage");

            Invoke("Damage2", afterHitTime);
        }

    }
    public void Damage2()
    {
        gotHit = false;
        canTakeDamage = true;
    }
    // Knockback Function
    public void Knockback(float knockDur, float knockPwr, Vector3 knockDir)
    {
        if (invincible) { return; }

        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

        while (knockDur > timer)
        {
            hasControl = false;
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockDir.x * (knockPwr / 2), knockDir.y * knockPwr, transform.position.z));
        }

        if (knockDur < timer)

        hasControl = true;

        return;
    }

    void OnTriggerEnter2D(Collider2D col){
    	
    	if (col.CompareTag("coin")){

    		Destroy(col.gameObject);
    		curHealth += 1;
    		gm.points += coinWorth;
    	}

    	if (col.CompareTag("death")){
    		Die();
    	}
    }
}
