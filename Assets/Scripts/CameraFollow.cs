using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.position.y > 6f)
        {
            if (player.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
    }
}
