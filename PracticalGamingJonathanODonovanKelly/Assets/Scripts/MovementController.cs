using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject CrossHair;
    focus_control cross_hair;
    float current_speed = 5;
    private float turning_speed = 360;
    float elevation_angle = 0;
    float aimSpeed = 1;
    float fireElapsedTime = 0;
    public float fireCooldown = 1;
    float jumpSpeed = 0.02f;
    CameraControl my_camera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject Crosshair_GO = Instantiate(CrossHair, transform.position, transform.rotation);
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
        if (should_turn_left()) turn_left();
        if (should_turn_right()) turn_right();
        if (should_sprint_forward()) sprint_forward();
        if (should_dodge_left()) dodge_left();
        if (should_dodge_right()) dodge_right();
        if (should_jump()) jump();
        fireElapsedTime += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireElapsedTime >= fireCooldown)
        {
            fireElapsedTime = 0;
            shoot_at(cross_hair);
        }
        elevation_angle = Mathf.Clamp(elevation_angle, -45f, 10f);
        cross_hair.update_elevation(elevation_angle);
    }

    // Movement methods

    private void shoot_at(focus_control cross_hair)
    {
        GameObject bullet = Instantiate(Bullet, transform.position+(transform.forward*1.1f), transform.rotation);
        bullet.transform.LookAt(cross_hair.transform.position);
        bullet.AddComponent<BulletControl>();
    }

    private void turn_right()
    {
        transform.Rotate(Vector3.up, turning_speed * Time.deltaTime);
    }

    private void jump()
    {
        transform.position += jumpSpeed * transform.up;
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


    private void dodge_left()
    {
        transform.position -= current_speed * transform.right * Time.deltaTime * 3;
    }


    private void dodge_right()
    {
        transform.position += current_speed * transform.right * Time.deltaTime * 3;
    }

    private void sprint_forward()
    {
        transform.position += current_speed * transform.forward * Time.deltaTime * 3;
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
        return Input.GetKey(KeyCode.E);
    }

    private bool should_turn_left()
    {
        return Input.GetKey(KeyCode.Q);
    }

    private bool should_strafe_right()
    {
        return Input.GetKey(KeyCode.D);
    }

    private bool should_dodge_right()
    {
        return Input.GetKey(KeyCode.K);
    }

    private bool should_jump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private bool should_strafe_left()
    {
        return Input.GetKey(KeyCode.A);
    }

    private bool should_dodge_left()
    {
        return Input.GetKey(KeyCode.J);
    }

    private bool should_sprint_forward()
    {
        return Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            elevation_angle -= aimSpeed;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            elevation_angle += aimSpeed;
        }

    }
   
}