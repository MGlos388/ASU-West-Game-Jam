using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    public AudioSource AcidHit;

    public Vector2 moveInput;
    public bool running;
    bool HurtingPlayer;
    bool dead = false;

    [SerializeField] public int woodcount = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    [SerializeField] GameObject deathscreen;


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

        spriteRenderer.color = new Color(Mathf.Lerp(
            spriteRenderer.color.r, 1, .01f),
            Mathf.Lerp(spriteRenderer.color.g, 1, .01f),
            Mathf.Lerp(spriteRenderer.color.b, 1, .01f));

        if (!dead)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            running = Input.GetKey(KeyCode.Z);
        }
        else {
            moveInput = Vector2.zero;
        }


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
            UpdateHealth(-sprintHealthDeduction);
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

        if (health <= 0&&!dead) {
            dead = true;
            StartCoroutine(death());
        }

        if (health <= 0) {
            health= 0;
            GetComponentInChildren<Light2D>().intensity -= Time.deltaTime / 3;
            deathscreen.transform.localScale = Vector3.Lerp(deathscreen.transform.localScale,Vector3.one*2,Time.deltaTime*2);

        }
    }

    IEnumerator death() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    
    }
    public void PopUpNumber(string textToShow)
    {
        StartCoroutine(PopUpNumber_Coro(transform.position + new Vector3(0, 2), textToShow,.5f));
    }
    IEnumerator PopUpNumber_Coro(Vector2 position, string textToShow, float duration)
    {
        GameObject popup = Instantiate(PopUpPrefab, transform.position, Quaternion.identity);
        popup.GetComponent<TextMeshPro>().text = textToShow;
        yield return new WaitForEndOfFrame();
        popup.GetComponent<TextMeshPro>().color = new Color32(255, 255, 255, 255);
        popup.transform.DOMove(position, 0.33f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(duration);
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
                    spriteRenderer.color = Color.red;
                    UpdateHealth(-10);
                    HurtingPlayer = true;
                    PopUpNumber("-10");
                }
                else if (coll.gameObject.CompareTag("Projectile"))
                {
                    spriteRenderer.color = Color.red;
                    Destroy(coll.gameObject); // for projectiles and mines, destroy them after a hit
                    UpdateHealth(-5); //errors if no healthbar, destroy obj first
                    HurtingPlayer = true;
                    AcidHit.Play();
                    PopUpNumber("-5");
                }
                else if (coll.gameObject.CompareTag("Mine"))
                {
                    StartCoroutine(CollideWithMine(coll.gameObject));
                }

            }
        }
        if (coll.gameObject.CompareTag("Material"))
        {
            UpdateHealth(10);
            Debug.Log("health");
            coll.gameObject.GetComponent<AudioSource>().Play();
            GameObject.Destroy(coll.gameObject);
        }
        if (coll.gameObject.CompareTag("Wood"))
        {
            UpdateWood(1);
            Debug.Log("wood");
            coll.gameObject.GetComponent<AudioSource>().Play();
            GameObject.Destroy(coll.gameObject, 0.5f);
        }
    }
    IEnumerator CollideWithMine(GameObject other)
    {
        other.GetComponent<AudioSource>().Play();
        spriteRenderer.color = Color.red;
        UpdateHealth(-15);
        PopUpNumber("-15");
        other.GetComponent<SpriteRenderer>().DOFade(0, 0.25f);
        HurtingPlayer = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(other);
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