using UnityEngine;
using System;
using System.Collections; 


public class CardScript : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isHovering = false;
    public float elevationAmount = 5f;
    public float speed = 2f;

    void Start()
    {
        // Guarda a posição original do cubo
        originalPosition = transform.position;
    }

    void OnMouseEnter()
    {
        // Quando o mouse entra no cubo
        isHovering = true;
    }

    void OnMouseExit()
    {
        // Quando o mouse sai do cubo
        isHovering = false;
    }

    void Update()
    {
       
        Vector3 targetPosition = isHovering ? 
            originalPosition + new Vector3(this.transform.position.x, this.transform.position.y * elevationAmount, this.transform.position.z) : 
            originalPosition;

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}