using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    // References
    private Rigidbody2D rb2d;
    private Animator anim;

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
    public int maxHealth = 5;  
    public int lastEnteredLevel;

    // Level up criteria
    public int pointsForLevel1 = 50;
    public int pointsForLevel2 = 100;
    public int pointsForLevel3 = 150;
    public int pointsForLevel4 = 200;

	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        // Healing the Player
        GameControl.control.curHealth = maxHealth;
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
                if (GameControl.control.canJumpInfinitely)
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

        // Health Management
        if (GameControl.control.curHealth > maxHealth)
        {
            GameControl.control.curHealth = maxHealth;
        }
        if (GameControl.control.curHealth <= 0)
        {
            StartCoroutine(Die());
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

    public IEnumerator Die() {

        lastEnteredLevel = SceneManager.GetActiveScene().buildIndex;
        GameControl.ChangeScene(lastEnteredLevel);
        yield return 0;
    }

    // Damage Function with optional Invincibility
    public void Damage(int dmg, int afterHitTime)
    {
        if (GameControl.control.invincible) { return; }
        if (canTakeDamage)
        {
            if (GameControl.control.curHealth < dmg)
            {
                GameControl.control.curHealth = 0;
                StartCoroutine(Die());
                return;
            }
            if (GameControl.control.hasInfiniteLives)
            {
                dmg = 0;
            }
            gotHit = true;
            GameControl.control.curHealth -= dmg;
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
        if (GameControl.control.invincible) { return; }

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
    		GameControl.control.curHealth += 1;
            GameControl.control.score += GameControl.control.coinWorth;

    	}

    	if (col.CompareTag("death")){
    		StartCoroutine(Die());
    	}
    }
}
