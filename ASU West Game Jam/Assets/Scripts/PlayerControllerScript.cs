using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float lerpSpeed;
    [SerializeField] float runSpeed;

    public float maxhealth;
    [SerializeField] float maxwater;
    [SerializeField] float sprintHealthDeduction;

    public float health;
    public Menu_Gameplay UI;

    private float invincibilityFrames = 30;

    [SerializeField] Rigidbody2D rb;

    public Vector2 moveInput;
    public bool running;
    bool HurtingPlayer;

    private void Start()
    {
        health = maxhealth;
        invincibilityFrames = 0.6f;
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

        if (HurtingPlayer)
        {
            invincibilityFrames -= Time.deltaTime;
            if (invincibilityFrames <= 0)
            {
                HurtingPlayer = false;
                invincibilityFrames = 0.6f;
            }
        }
        Debug.Log(health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!HurtingPlayer)
        {
            if (collision.gameObject.CompareTag("Enemy") && invincibilityFrames == 0.6f)
            {
                UI.UpdateHealth();
                health -= 10;
                HurtingPlayer = true;
            }
        }  
    }

    public void UpdateHealth(float h) {
        health += h;
        if (health>=maxhealth) {
            health = maxhealth;
        }
    }

}