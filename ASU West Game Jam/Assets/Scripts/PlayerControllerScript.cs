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
    public GameManager UI;

    public float invincibilityTime;
    private float invincibilityTime_Elapsed;

    [SerializeField] Rigidbody2D rb;

    public Vector2 moveInput;
    public bool running;
    bool HurtingPlayer;

    public int woodcount = 0;

    private void Start()
    {
        health = maxhealth;
        invincibilityTime_Elapsed = invincibilityTime;
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
            invincibilityTime_Elapsed -= Time.deltaTime;
            if (invincibilityTime_Elapsed <= 0)
            {
                HurtingPlayer = false;
                invincibilityTime_Elapsed = invincibilityTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!HurtingPlayer)
        {
            if (coll.gameObject.CompareTag("Enemy") && invincibilityTime_Elapsed == invincibilityTime)
            {
                UpdateHealth(-10);
                HurtingPlayer = true;
            }
        }
        if (coll.gameObject.CompareTag("Material"))
        {
            UpdateHealth(10);
            Debug.Log("health");
            GameObject.Destroy(coll.gameObject);
        }
        if (coll.gameObject.CompareTag("Wood"))
        {
            UpdateWood(1);
            Debug.Log("wood");
            GameObject.Destroy(coll.gameObject);
        }
    }

    public void UpdateHealth(float h) {
        health += h;
        if (health >= maxhealth) {
            health = maxhealth;
        }
        UI.UpdateHealth();
    }

    public void UpdateWood(int w)
    {
        woodcount += w;
        UI.UpdateWood();

    }

}