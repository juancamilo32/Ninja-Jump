using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform player;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player.position.y > 6f)
        {
            if (player.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, 0.3f);
            }
        }
    }
}
