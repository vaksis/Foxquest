/**********************************************
 * Summary 4.11.2021
 *
 * PlayerController.cs is a player controller wich
 * controls all the variables that player does.
 *
 *************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator m_Animator;
    //===========MOVEMENT===========
    public float speed;
    public float jumpforce;
    public float moveInput;


    private Rigidbody2D rb;
    public int extraJumps;
    private int extraJumpValue = 2;

    //===========FLIP===========
    private bool facingRight = true;

    //===========GROUNDCHECK===========
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //===========SCORE===========
    public int score;
    public Text txtScore;

    //===========HEALTH===========
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        //===========HEALTH===========
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //===========MOVEMENT===========
        rb = GetComponent<Rigidbody2D>();

        //===========SCORE===========
        score = 0;
        txtScore.text = ": " + score;

        m_Animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //===========JUMP===========
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            m_Animator.SetBool("Jump", true);
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
    }

    void Flip()
    {
        //===========FLIP THE PLAYERMODEL===========
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {

        //if player falls from the map it returns gameover scene
        if (rb.transform.position.y < -10)
        {
            SceneManager.LoadScene("GameOver");
        }

        //===========MOVEMENT===========
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        // oikea nuoli = 1, vasen nuoli = -1
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (!facingRight && moveInput > 0)
        {
            // jos ei katsota oikeaan ja painettu oikealle
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            // tai jos katsotaan oikealle ja painettu vasemmalle
            Flip();
        }

        if (moveInput < 0)
        {
            m_Animator.SetBool("Run_Left", true);
        }

        if (moveInput > 0)
        {
            m_Animator.SetBool("Run_Right", true);
        }

        if (moveInput == 0)
        {
            m_Animator.SetBool("Run_Left", false);
            m_Animator.SetBool("Run_Right", false);
        }

        if (isGrounded == true && rb.velocity.y == 0)
        {
            m_Animator.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            m_Animator.Play("Base Layer.Jump", -1, 0f);
        }
    }



    //===========On collision enter 2D===========
    void OnTriggerEnter2D(Collider2D col)
    {
        //Loads new level when player collides with object tag "House"
        if (col.gameObject.tag == "House")
        {
            SceneManager.LoadScene("lvlClear");
        }


        if(col.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
            if (currentHealth == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }


        //Collect score when player collides with object tag "Diamond"
        if (col.gameObject.tag == "Diamond")
        {
            score++;
            // kasvata pisteitä yhdellä
            Debug.Log("score: " + score);
            txtScore.text = ": " + score; 
            col.gameObject.SetActive(false);
        }
    }

    //===========Player takes damage===========
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
