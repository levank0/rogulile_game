using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveSpeed;
    /*private Animator anim;*/
    private int maxHealth = 100;
    public int currentHealth;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*anim = GetComponent<Animator>();*/
        currentHealth = maxHealth;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("potion"))
        {
            
            currentHealth= currentHealth + 20;


            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // получаем значения клавиш влево/вправо
        float verticalInput = Input.GetAxis("Vertical"); // получаем значения клавиш вверх/вниз
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput); // создаем вектор движения
        
        moveSpeed = moveDirection * speed;
        /*if (moveDirection.x == 0)
        {
            anim.SetBool("isrunner", false);
        }
        else
        {
            anim.SetBool("isrunner", true);
        }*/
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime);
    }
    
}