using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float camera_distance = 10;
    float camera_height = 3;

    Transform character, focus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desired_position = character.position - camera_distance * character.forward + camera_height * Vector3.up;

        transform.position = Vector3.Lerp(transform.position, desired_position, 0.2f);
        transform.LookAt(focus.position, Vector3.up);
    }

    internal void Link(Transform character_transform, Transform crosshairs)
    {
        character = character_transform;
        focus = crosshairs;
    }
}