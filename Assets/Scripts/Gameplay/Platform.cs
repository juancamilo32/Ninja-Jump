using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    int id;

    GameObject player;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0)
        {
            Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();
            Animator animator = other.collider.GetComponent<Animator>();
            if (rb)
            {
                if (id == 0 || id == 1)
                {
                    if (animator)
                    {
                        animator.SetTrigger("HitGround");
                    }
                    if (id == 0)
                    {
                        AudioManager.instance.Play("Jump");
                    }
                    else
                    {
                        AudioManager.instance.Play("Bouncy");
                    }
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }
                else if (id == 2)
                {
                    Rigidbody2D rb2 = GetComponent<Rigidbody2D>();
                    if (rb2)
                    {
                        rb2.gravityScale = 1;
                    }
                    AudioManager.instance.Play("Falloff");
                }
                else if (id == 3)
                {
                    Player player = other.collider.GetComponent<Player>();
                    if (player)
                    {
                        StartCoroutine(DeathRoutine(animator, player));
                    }
                }
            }

        }
    }

    IEnumerator DeathRoutine(Animator animator, Player player)
    {
        animator.SetTrigger("Death");
        player.canMove = false;
        if (!player.deathAudioPlayed)
        {
            AudioManager.instance.Play("Death");
            player.deathAudioPlayed = true;
        }
        yield return new WaitForSeconds(1f);
        player.dead = true;
    }

    private void Update()
    {
        if (id != 0)
        {
            if (player.transform.position.y > transform.position.y + 8)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
