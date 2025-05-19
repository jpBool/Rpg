using UnityEngine;
using System;
using System.Collections; 

public class CamScript : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    [Range(0f, 0.5f)] public float edgeThreshold = 0.05f; // Margem da borda da tela (5%)

    [Header("Vertical Limits")]
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private float distance;
    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraOrbit: Target not assigned!");
            return;
        }

        distance = Vector3.Distance(transform.position, target.position);
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            
            // Verifica se o mouse está nas bordas da tela
            bool onEdge = 
                mousePos.x <= screenSize.x * edgeThreshold || 
                mousePos.x >= screenSize.x * (1f - edgeThreshold) ||
                mousePos.y <= screenSize.y * edgeThreshold || 
                mousePos.y >= screenSize.y * (1f - edgeThreshold);

            if (onEdge)
            {
                // Calcula a direção do movimento baseado na posição do mouse
                Vector2 edgeDirection = new Vector2(
                    (mousePos.x / screenSize.x).Remap(0f, 1f, -1f, 1f),
                    (mousePos.y / screenSize.y).Remap(0f, 1f, -1f, 1f)
                ).normalized;

                x += edgeDirection.x * xSpeed * distance * Time.deltaTime;
                y -= edgeDirection.y * ySpeed * Time.deltaTime;
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}

// Extensão para facilitar o mapeamento de valores
public static class ExtensionMethods
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}