using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Joystick_L1 joystick_l;
    private Joystick_R1 joystick_r1;
    private Joystick_R2 joystick_r2;

    private Player player;
    Rigidbody rigid_body;

    //Left joystick. Used to move the player
    public float stop_duration = 0.4f;
    private float speed_factor;
    private float velo_x = 0f;
    private float velo_z = 0f;
    private float rot_y = 0f;
    private float curr_time = 0;
    private float fix_vel_x;
    private float fix_vel_z;
    private float last_rot;
    private Vector2 direction_l;
    private Vector2 direction_l_no_normal;
    private Vector3 velo_eq;

    //Used to detect joystick
    public float sensibility = 0.3f;

    //Attack variables
    public float cancel_attack_r1 = 0.5f;
    public float last_r1 = 1f;
    [HideInInspector] public Vector2 direction_r1;
    [HideInInspector] public Vector2 direction_r1_no_normal;
    [HideInInspector] public Vector2 last_direction_r1_no_normal;

    public float cancel_attack_r2 = 0.2f;
    public float last_r2 = 0f;
    [HideInInspector] public Vector2 direction_r2;
    [HideInInspector] public Vector2 direction_r2_no_normal;
    [HideInInspector] public Vector2 last_direction_r2_no_normal;

    public void SetSpeedFactor(float new_speed)
    {
        speed_factor = new_speed;
    }

    void MovingPlayer()
    {
        velo_x = joystick_l.Horizontal * speed_factor;
        velo_z = joystick_l.Vertical * speed_factor;
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

    void Rotation(Vector2 direction, Joystick_L1 _joystick_l = null, Joystick_R1 _joystick_r1 = null, Joystick_R2 _joystick_r2 = null)
    {
        rot_y = Mathf.Asin(-direction.y) * Mathf.Rad2Deg;

        if (_joystick_l != null)
        {
            if (_joystick_l.Horizontal >= 0)
            {
                transform.rotation = Quaternion.Euler(0, rot_y, 0);
                last_rot = rot_y;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180 - rot_y, 0);
                last_rot = rot_y;
            }
        }

        if (_joystick_r1 != null)
        {
            last_direction_r1_no_normal = direction_r1_no_normal; //Still not in use
            if (_joystick_r1.Horizontal >= 0)
            {
                transform.rotation = Quaternion.Euler(0, rot_y, 0);
                last_rot = rot_y;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180 - rot_y, 0);
                last_rot = rot_y;
            }
        }

        if (_joystick_r2 != null)
        {
            last_direction_r2_no_normal = direction_r2_no_normal;
            if (_joystick_r2.Horizontal >= 0)
            {
                transform.rotation = Quaternion.Euler(0, rot_y, 0);
                last_rot = rot_y;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180 - rot_y, 0);
                last_rot = rot_y;
            }
        }
    }

    void RotatingPlayer()
    {
        if (direction_r1.magnitude == 0 && direction_l_no_normal.magnitude > 0 && direction_r2.magnitude == 0)
        {
            Rotation(direction_l, joystick_l);
        }
        else if (direction_r1.magnitude > 0 && direction_l_no_normal.magnitude == 0 && direction_r2.magnitude == 0)
        {
            Rotation(direction_r1, null, joystick_r1);
        }
        else if (direction_r1.magnitude == 0 && direction_l_no_normal.magnitude == 0 && direction_r2.magnitude > 0)
        {
            Rotation(direction_r2, null, null, joystick_r2);
        }
    }

    void StaticRotation()
    {
        if (direction_r1_no_normal.magnitude > 0 && direction_r2_no_normal.magnitude == 0)
        {
            Rotation(direction_r1, null, joystick_r1);
        }
        else if (direction_r1_no_normal.magnitude == 0 && direction_r2_no_normal.magnitude > 0)
        {
            Rotation(direction_r2, null, null, joystick_r2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        joystick_l = FindObjectOfType<Joystick_L1>();
        joystick_r1 = FindObjectOfType<Joystick_R1>();
        joystick_r2 = FindObjectOfType<Joystick_R2>();

        rigid_body = GetComponent<Rigidbody>();
        player = GetComponent<Player>();

        velo_eq = new Vector3(0, 0, 0);
        direction_l = new Vector2(0, 0);
        direction_l_no_normal = new Vector2(0, 0);

        direction_r1 = new Vector2(0, 0);
        direction_r1_no_normal = new Vector2(0, 0);

        direction_r2 = new Vector2(0, 0);
        direction_r2_no_normal = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction_l.Set(joystick_l.Horizontal, joystick_l.Vertical);
        direction_l.Normalize();
        direction_l_no_normal.Set(joystick_l.Horizontal, joystick_l.Vertical);

        direction_r1.Set(joystick_r1.Horizontal, joystick_r1.Vertical);
        direction_r1.Normalize();
        direction_r1_no_normal.Set(joystick_r1.Horizontal, joystick_r1.Vertical);

        direction_r2.Set(joystick_r2.Horizontal, joystick_r2.Vertical);
        direction_r2.Normalize();
        direction_r2_no_normal.Set(joystick_r2.Horizontal, joystick_r2.Vertical);

        player.StraightAiming();
        player.AoEAiming();

        if (direction_l.magnitude >= sensibility)
        {
            RotatingPlayer();
            MovingPlayer();
            curr_time = 0;
            fix_vel_x = velo_x;
            fix_vel_z = velo_z;
        }
        else
        {
            StaticRotation();
            StoppingPlayer();
        }
    }
}

