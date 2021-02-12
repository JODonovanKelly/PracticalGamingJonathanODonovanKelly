using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focus_control : MonoBehaviour
{
    private float distance_to_crosshair = 3;
    float elevation;
    Transform owner;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = 0.5f * Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desired_position = owner.position + Quaternion.AngleAxis(elevation, owner.right) * (owner.forward * distance_to_crosshair);
        //transform.RotateAround(owner.position, owner.right, elevation);
        transform.position = Vector3.Lerp(transform.position, desired_position, 0.2f);
        transform.rotation = owner.transform.rotation;
    }

    internal void update_elevation(float latest_elevation)
    {
        elevation = latest_elevation;
    }

    internal void starting_setup(Transform character)
    {
        owner = character;
    }
}