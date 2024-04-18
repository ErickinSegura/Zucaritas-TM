using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public float parallaxSpeed = 0.5f;

    private float spriteWidth, startPos;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position.x;
    }

    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - lastCameraPosition.x) * parallaxSpeed;
        float moveAmount = cameraTransform.position.x * (1 - parallaxSpeed);
        transform.Translate(new Vector3(deltaX, 0f, 0f));
        lastCameraPosition = cameraTransform.position;

        if (moveAmount > startPos + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0f, 0f));
            startPos += spriteWidth;
        }

        else if (moveAmount < startPos - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0f, 0f));
            startPos -= spriteWidth;
        }

        
    }
}
