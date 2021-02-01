using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (should_move_forward()) move forward();


    }

    private void move_forward()
    {
        transform.position += transform.forward;
    }

    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }
}
