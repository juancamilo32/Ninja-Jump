using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float horizontalInput = 0f;
    float xLimit = 3.1f;
    float highestY = 0;
    int score = 0;
    int bestScore = 0;

    public bool dead = false;
    public bool deathAudioPlayed = false;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bestScore = PlayerPrefs.GetInt("Score", 0);
        UIManager.Instance.UpdateBestScore(bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        animator.SetFloat("yVel", rb.velocity.y);
        if (transform.position.y < (highestY - 7f))
        {
            dead = true;
        }
        if (dead)
        {
            Death();
        }
        if (canMove)
        {
            Movement();
        }

    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontalInput;
        rb.velocity = velocity;
    }

    void UpdateScore()
    {
        if (transform.position.y > highestY)
        {
            highestY = transform.position.y;
            score = Mathf.FloorToInt(highestY * 5);
        }
        if (transform.position.y > 6)
        {
            UIManager.Instance.UpdateScore(score);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (transform.position.x >= xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        }
    }

    public void Death()
    {
        if (!deathAudioPlayed)
        {
            AudioManager.instance.Play("Death");
            deathAudioPlayed = true;
        }
        UIManager.Instance.EnableDeathScreen(score);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Score", bestScore);
            UIManager.Instance.UpdateBestScore(bestScore);
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.RestartGame();
        }
    }

    public IEnumerator DeathRoutine()
    {
        canMove = false;
        animator.SetTrigger("Death");
        if (!deathAudioPlayed)
        {
            AudioManager.instance.Play("Death");
            deathAudioPlayed = true;
        }
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        dead = true;
    }

}
