using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    [Range(1, 10)]
    public float jumpForce;
    public Rigidbody2D rb2D;
    private bool isGrounded;

    public bool canDoubleJump;

    public Animator animator;
    public Image filler;
    public float counter;
    public float maxCounter;
    private bool isNearBonfire = false;
    private Vector3 startPosition;

    // public AudioClip footstepSound;
    public AudioClip jumpSound;
    private AudioSource audioSource;



    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();

    }


    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
            if (isGrounded && !audioSource.isPlaying)
            {
                // audioSource.clip = footstepSound;
                audioSource.Play();
            }

        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isGrounded)
            {
                // This part handles the case when the player is on the ground
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
                animator.SetTrigger("Jump");
                audioSource.PlayOneShot(jumpSound);
                isGrounded = false; // Player is now in the air
                canDoubleJump = true; // Enable double jump
            }
            else if (canDoubleJump)
            {
                // This part handles the double jump
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
                animator.SetTrigger("Jump");
                audioSource.PlayOneShot(jumpSound);
                canDoubleJump = false; // Disable further double jumps
            }
        }



        if (counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);

        if (isNearBonfire)
        {
            int healingAmount = 10;
            if (GameManager.manager.health < GameManager.manager.maxHealth)
            {
                IncHealth(healingAmount * Time.deltaTime);
            }
        }

        if (transform.position.y < -10f)
        {
            Respawn();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(20);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = false;
        }
    }
    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
        if (collision.CompareTag("Bonfire"))
        {
            isNearBonfire = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonfire"))
        {
            isNearBonfire = false;
        }
    }
    public void IncHealth(float healthValue)
    {
        GameManager.manager.health += healthValue; // Increase health
    }
    void Respawn()
    {
        rb2D.velocity = Vector2.zero;
        transform.position = startPosition;
        GameManager.manager.health -= 5;
    }




}
