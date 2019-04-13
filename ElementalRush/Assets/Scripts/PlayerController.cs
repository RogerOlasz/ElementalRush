﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Joystick joystick;

    float speed_factor;
    private float velo_x = 0f;
    private float velo_z = 0f;

    public float stop_duration = 0.4f;
    private Vector3 velo_eq;
    float curr_time = 0;
    float fix_vel_x;
    float fix_vel_z;

    Rigidbody rigid_body;

    public void SetSpeedFactor(float new_speed)
    {
        speed_factor = new_speed;
    }

    void MovingPlayer()
    {
        velo_x = joystick.Horizontal * speed_factor;
        velo_z = joystick.Vertical * speed_factor;
        velo_eq.Set(velo_x, 0, velo_z);
        rigid_body.velocity = velo_eq;
    }

    void StoppingPlayer()
    {
        curr_time += Time.fixedDeltaTime;

        if (curr_time <= stop_duration)
        {
            //Current time during desaceleration
            //Debug.Log(curr_time);
            velo_x = fix_vel_x - fix_vel_x * curr_time / stop_duration;
            velo_z = fix_vel_z - fix_vel_z * curr_time / stop_duration;
            velo_eq.Set(velo_x, 0, velo_z);
            rigid_body.velocity = velo_eq;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rigid_body = GetComponent<Rigidbody>();
        velo_eq = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joystick.Vertical != 0f || joystick.Horizontal != 0f)
        {
            MovingPlayer();

            curr_time = 0;
            fix_vel_x = velo_x;
            fix_vel_z = velo_z;
        }
        else
        {
            StoppingPlayer();
        }
        //Player Velocity
        //Debug.Log(rigidbody.velocity);
    }    
}

