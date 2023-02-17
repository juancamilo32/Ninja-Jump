using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{

    [SerializeField]
    float fallingSpeed = 10f;
    [SerializeField]
    float rotationSpeed = 10f;

    GameObject player;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * fallingSpeed;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        if (player.transform.position.y > transform.position.y + 8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            StartCoroutine(player.DeathRoutine());
            this.gameObject.SetActive(false);
        }
    }

}
