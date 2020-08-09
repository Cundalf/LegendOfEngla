using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Tooltip("Objetivo que seguira la camara")]
    public GameObject target;
    [Tooltip("Velocidad de movimiento de la camara")]
    public float cameraSpeed;

    private Vector3 targetPosition;
    private Camera theCamera;
    private Vector3 minLimits, maxLimits;

    // Punto central de la camara
    private float halfHeight, halfWidth;

    void Update()
    {
        // Calculo de posicion para el target
        float posX = Mathf.Clamp(target.transform.position.x, minLimits.x + halfWidth, maxLimits.x - halfWidth);
        float posY = Mathf.Clamp(target.transform.position.y, minLimits.y + halfHeight, maxLimits.y - halfHeight);

        targetPosition = new Vector3(posX, posY, transform.position.z);
    }

    private void LateUpdate()
    {
        // Movimiento de la camara
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }

    public void ChangeLimits(BoxCollider2D cameraLimits)
    {
        // Limites de la propia camara
        minLimits = cameraLimits.bounds.min;
        maxLimits = cameraLimits.bounds.max;

        // Calculo del punto central de la camara
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = (halfHeight / Screen.height) * Screen.width;
    }
}
