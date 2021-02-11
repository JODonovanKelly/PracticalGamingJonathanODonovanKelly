using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    focus_control cross_hair;
    float current_speed = 5;
    private float turning_speed = 180;
    float turning_sensitivity = 20;
    float elevation_angle = 0;
    CameraControl my_camera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject Crosshair_GO = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cross_hair = Crosshair_GO.AddComponent<focus_control>();
        cross_hair.starting_setup(transform);
        my_camera = Camera.main.GetComponent<CameraControl>();
        my_camera.Link(transform, cross_hair.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Implement motion
        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        if (should_strafe_left()) strafe_left();
        if (should_strafe_right()) strafe_right();
        //if (should_turn_left()) turn_left();
        //if (should_turn_right()) turn_right();
        if (Input.GetButton("Fire1")) shoot_at(cross_hair);

        turn(Input.GetAxis("Horizontal"));

        elevation_angle -= Input.GetAxis("Vertical");
        elevation_angle = Mathf.Clamp(elevation_angle, -45f, 45f);
        cross_hair.update_elevation(elevation_angle);

    }

    // Movement methods

    private void shoot_at(focus_control cross_hair)
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(cross_hair.transform.position);
        bullet.AddComponent<projectile_control>();

    }
    private void turn(float Input_axis_value)
    {
        transform.Rotate(Vector3.up, turning_sensitivity * Input_axis_value * Time.deltaTime);
    }
    private void turn_right()
    {
        transform.Rotate(Vector3.up, turning_speed * Time.deltaTime);
    }

    private void turn_left()
    {
        transform.Rotate(Vector3.up, -turning_speed * Time.deltaTime);
    }

    private void strafe_right()
    {
        transform.position += current_speed * transform.right * Time.deltaTime;
    }

    private void strafe_left()
    {
        transform.position -= current_speed * transform.right * Time.deltaTime;
    }


    /// <summary>
    /// Move the gameobject forward relative to its own orientation
    /// </summary>
    private void move_forward()
    {

        transform.position += current_speed * transform.forward * Time.deltaTime;
    }
    private void move_backward()
    {

        transform.position -= current_speed * transform.forward * Time.deltaTime;
    }

    // User input for movement
    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }


    private bool should_turn_right()
    {
        return Input.GetKey(KeyCode.D);
    }

    private bool should_turn_left()
    {
        return Input.GetKey(KeyCode.A);
    }

    private bool should_strafe_right()
    {
        return Input.GetKey(KeyCode.E);
    }

    private bool should_strafe_left()
    {
        return Input.GetKey(KeyCode.Q);
    }
}