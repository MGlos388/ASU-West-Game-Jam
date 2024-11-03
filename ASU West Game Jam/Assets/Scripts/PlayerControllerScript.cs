using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject PopUpPrefab;

    public float invincibilityTime;
    private float invincibilityTime_Elapsed;

    [SerializeField] Rigidbody2D rb;

    public Vector2 moveInput;
    public bool running;
    bool HurtingPlayer;

    [SerializeField] public int woodcount = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    private void Start()
    {
        // Reference to Animator and SpriteRenderer components
        animator = transform.Find("SBMSPlayer1").GetComponent<Animator>();
        spriteRenderer = transform.Find("SBMSPlayer1").GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();

        health = maxhealth;
        invincibilityTime_Elapsed = invincibilityTime;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        running = Input.GetKey(KeyCode.Z);


        moveInput.Normalize();

        // Flip the sprite based on horizontal movement
        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }

        // Control animations based on movement
        if (moveInput.magnitude > 0)
        {
            // animator.SetBool("isMoving", true);
            animator.enabled = true; // Enable animation when moving
        }
        else
        {
            // animator.SetBool("isMoving", false);
            animator.enabled = false; // disable animation when moving
        }

        Vector2 movetarget = moveInput * moveSpeed;
        if (running && health > 0 && moveInput != Vector2.zero)
        {
            movetarget *= runSpeed;
            health -= sprintHealthDeduction;
        }

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, movetarget, lerpSpeed);

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
    public void PopUpNumber(string textToShow)
    {
        StartCoroutine(PopUpNumber_Coro(transform.position + new Vector3(0, 2), textToShow));
    }
    IEnumerator PopUpNumber_Coro(Vector2 position, string textToShow)
    {
        GameObject popup = Instantiate(PopUpPrefab, transform.position, Quaternion.identity);
        popup.GetComponent<TextMeshPro>().text = textToShow;
        yield return new WaitForEndOfFrame();
        popup.GetComponent<TextMeshPro>().color = new Color32(255, 255, 255, 255);
        popup.transform.DOMove(position, 0.33f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(0.5f);
        popup.GetComponent<TextMeshPro>().DOFade(0, 0.15f);
        Destroy(popup, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("oncoll player w tag " + coll.gameObject.tag);
        if (!HurtingPlayer)
        {
            if (invincibilityTime_Elapsed == invincibilityTime)
            {
                if (coll.gameObject.CompareTag("Enemy"))
                {
                    UpdateHealth(-10);
                    HurtingPlayer = true;
                    PopUpNumber("-10");
                }
                else if (coll.gameObject.CompareTag("Projectile"))
                {
                    Destroy(coll.gameObject); // for projectiles and mines, destroy them after a hit
                    UpdateHealth(-5); //errors if no healthbar, destroy obj first
                    HurtingPlayer = true;
                    PopUpNumber("-5");
                }
                else if (coll.gameObject.CompareTag("Mine"))
                {
                    Destroy(coll.gameObject);
                    UpdateHealth(-15);
                    HurtingPlayer = true;
                    PopUpNumber("-15");
                }

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

    public void UpdateHealth(float h)
    {
        health += h;
        if (health >= maxhealth)
        {
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