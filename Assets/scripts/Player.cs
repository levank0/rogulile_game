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
    public int health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*anim = GetComponent<Animator>();*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 10; // уменьшаем здоровье игрока при столкновении с врагом
        }
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
        if(health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime);
    }
    public void ChangeHealth(int healthValue)
    {
        health += healthValue;

    }
}