using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject camera;
    public float speed = 0.1f;




    // Update is called once per frame
    void Update()
    {
        Vector3 pos = camera.transform.position;
        pos.x += speed;
        camera.transform.position = pos;
    }
}
