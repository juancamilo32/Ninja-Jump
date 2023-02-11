using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0f;
    Rigidbody2D rb;
    float horizontalInput = 0f;
    float xLimit = 3.1f;
    float highestY = 0;
    int score = 0;
    int bestScore = 0;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bestScore = PlayerPrefs.GetInt("Score", 0);
        UIManager.Instance.UpdateBestScore(bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        UpdateScore();
        if (transform.position.y < (highestY - 7f))
        {
            dead = true;
        }
        if (dead)
        {
            StartCoroutine(DeathRoutine());
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
        if (transform.position.x >= xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        }
    }

    public IEnumerator DeathRoutine()
    {
        // Play death animation
        yield return new WaitForSeconds(1f);
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

}
