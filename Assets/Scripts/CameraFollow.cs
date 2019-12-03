using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPosition;
    public float cameraSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}
