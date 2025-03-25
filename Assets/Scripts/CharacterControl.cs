using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Animator animator;

    public Rigidbody2D rb2D;
    public float health;
    public float previousHealth;
    public float maxHealth;
    public Image filler;
    public float counter;
    public float maxCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
        }

        if (counter > maxCounter)
        {
            previousHealth = health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(previousHealth / maxHealth, health / maxHealth, counter / maxCounter);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(float dmg)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
    }
}
