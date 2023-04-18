using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    private float startTimeBtwAttack;
    public int health;
    public float speed;
    public GameObject deathEffect;
    public int damage;
    private float stoptime;
    public float startStopTime;
    public float normalSpeed;
    private Player player;
    private Animator anim;
    public LayerMask enemy;
    public float attackRange;
    public Transform attackPos;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
    }

    private void Update()
    {
        if(stoptime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stoptime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        stoptime = startStopTime;
        Instantiate(deathEffect, transform.position, quaternion.identity);
        health -= damage;
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < 4; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    public void OnEnemyAttack()
    {
        Instantiate(deathEffect,player.transform.position, Quaternion.identity);
        player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }
    
        






}
