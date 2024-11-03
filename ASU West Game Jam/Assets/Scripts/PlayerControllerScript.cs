using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float lerpSpeed;
    [SerializeField] float runSpeed;

    [SerializeField] float maxhealth;
    [SerializeField] float maxwater;
    [SerializeField] float sprintHealthDeduction;

    private float health;

    private float invincibilityFrames = 0;

    [SerializeField] Rigidbody2D rb;

    public Vector2 moveInput;
    public bool running;

    private void Start()
    {
        health = maxhealth;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        running = Input.GetKey(KeyCode.Z);


        moveInput.Normalize();

        Vector2 movetarget = moveInput * moveSpeed * Time.deltaTime;
        if (running && health>0 && moveInput!=Vector2.zero) {
            movetarget *= runSpeed;
            health -= sprintHealthDeduction;


         }

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity,movetarget,lerpSpeed);

        Debug.Log(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("you collided entered");
        if (collision.gameObject.tag == "Enemy" && invincibilityFrames <= 0)
        {
            invincibilityFrames = 30;
            health -= 10;


        }
        else {
            invincibilityFrames -= Time.deltaTime;
        }
    
    }

    public void UpdateHealth(float h) {
        health += h;
        if (health>=maxhealth) {
            health = maxhealth;
        }
    }

}