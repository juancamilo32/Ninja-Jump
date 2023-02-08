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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        UpdateScore();
        ManageDeath();
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

    void ManageDeath()
    {
        if (transform.position.y < (highestY - 7f))
        {
            UIManager.Instance.EnableDeathScreen(score);
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.RestartGame();
            }
        }
    }
}
