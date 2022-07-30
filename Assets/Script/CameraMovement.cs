using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 cameraAim;
    private Transform aim;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player")!=null) {
            aim = GameObject.FindGameObjectWithTag ("Player").transform;
        }
    }


    void Update()
    {
        if (aim!=null) {
            cameraAim = new Vector3(aim.position.x,transform.position.y,aim.position.z);
            transform.position = Vector3.Lerp(transform.position,cameraAim,Time.deltaTime*8);
        }
    }
}
