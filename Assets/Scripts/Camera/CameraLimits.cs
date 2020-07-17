using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraLimits : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<CameraFollow>().ChangeLimits(GetComponent<BoxCollider2D>());
    }
}
