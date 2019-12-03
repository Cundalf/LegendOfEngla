using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string uuid;
    private PlayerController player;
    private CameraFollow myCamera;
    public Vector2 facingDirection = Vector2.zero;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        myCamera = FindObjectOfType<CameraFollow>();

        if( !player.nextUUID.Equals(uuid) )
        {
            return;

        }

        player.transform.position = transform.position;
        myCamera.transform.position = new Vector3(transform.position.x, transform.position.y, myCamera.transform.position.z);

        player.lastMovement = facingDirection;
    }
}
