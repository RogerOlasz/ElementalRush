using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    private Joystick_L1 left_joystick;
    private Joystick_R1 right_straight_joystick;
    private Joystick_R2 right_aoe_joystick;

    private Player player;
    private Rigidbody rigid_body;

    [HideInInspector] public bool velocity_control;    

    //Left joystick. Used to move the player
    [SerializeField] private float stop_duration = 0.4f;
    [SerializeField] private float left_joystick_sensibility = 0.3f;
    private float movement_speed_factor = 0;
    private float stopping_current_time = 0;
    private Vector3 velocity;
    private Vector3 current_velocity;

    private bool straight_aiming = false;
    [SerializeField] private float right_1_joystick_sensibility = 0.3f;

    private bool aoe_aiming = false;
    [SerializeField] private float right_2_joystick_sensibility = 0.3f;

    Vector3 looking_at;
    private Vector3 real_position;
    private Quaternion real_rotation;

    public void SetSpeedFactor(float new_movement_speed)
    {
        if (photonView.IsMine)
        {
            movement_speed_factor = new_movement_speed;
        }
    }

    private void MovingPlayer()
    {
        if (photonView.IsMine)
        {
            if (!velocity_control)
            {
                if (gameObject.layer == LayerMask.NameToLayer("TeamBlue"))
                {
                    velocity.x = left_joystick.Horizontal() * movement_speed_factor;
                    velocity.z = left_joystick.Vertical() * movement_speed_factor;
                }
                else
                {
                    velocity.x = left_joystick.Horizontal() * movement_speed_factor * -1;
                    velocity.z = left_joystick.Vertical() * movement_speed_factor * -1;                    
                }

                rigid_body.velocity = velocity;

                if (velocity != Vector3.zero && straight_aiming == false && aoe_aiming == false)
                {
                    rigid_body.rotation = Quaternion.LookRotation(velocity);
                }
            } 
        }
    }

    private void StoppingPlayer()
    {
        if (photonView.IsMine)
        {
            if (!velocity_control)
            {
                stopping_current_time += Time.fixedDeltaTime;

                if (stopping_current_time <= stop_duration)
                {
                    if (gameObject.layer == LayerMask.NameToLayer("TeamBlue"))
                    {
                        velocity.x = current_velocity.x - current_velocity.x * stopping_current_time / stop_duration;
                        velocity.z = current_velocity.z - current_velocity.z * stopping_current_time / stop_duration;
                    }
                    else
                    {
                        velocity.x = (current_velocity.x - current_velocity.x * stopping_current_time / stop_duration) * -1;
                        velocity.z = (current_velocity.z - current_velocity.z * stopping_current_time / stop_duration) ;
                    }

                    rigid_body.velocity = velocity;

                    if (velocity != Vector3.zero && straight_aiming == false && aoe_aiming == false)
                    {
                        rigid_body.rotation = Quaternion.LookRotation(velocity);
                    }
                }
            }
        }
    }    

    public void StraightAiming()
    {
        looking_at.x = right_straight_joystick.Horizontal();
        looking_at.z = right_straight_joystick.Vertical();

        rigid_body.rotation = Quaternion.LookRotation(looking_at);
        straight_aiming = true;
    }

    public void AoEAiming()
    {
        looking_at.x = right_aoe_joystick.Horizontal();
        looking_at.z = right_aoe_joystick.Vertical();

        rigid_body.rotation = Quaternion.LookRotation(looking_at);
        aoe_aiming = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            left_joystick = FindObjectOfType<Joystick_L1>();
            right_straight_joystick = FindObjectOfType<Joystick_R1>();
            right_aoe_joystick = FindObjectOfType<Joystick_R2>();

            rigid_body = GetComponent<Rigidbody>();
            player = GetComponent<Player>();

            velocity = Vector3.zero;
            current_velocity = Vector3.zero;
            looking_at = Vector3.zero;

            velocity_control = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            //player.StraightAiming();
            //player.AoEAiming();

            if (left_joystick.Direction().magnitude >= left_joystick_sensibility)
            {
                MovingPlayer();
                stopping_current_time = 0;
                current_velocity.x = velocity.x;
                current_velocity.z = velocity.z;
            }
            else if (rigid_body.velocity != Vector3.zero)
            {
                StoppingPlayer();
            }

            if (right_straight_joystick.Direction().magnitude > right_1_joystick_sensibility && aoe_aiming == false)
            {
                StraightAiming();
                //player.StraightAimingScheme();
            }
            else if (straight_aiming == true)
            {
                player.StraightAttack();
                straight_aiming = false;
            }

            if (right_aoe_joystick.Direction().magnitude > right_2_joystick_sensibility && straight_aiming == false)
            {
                AoEAiming();
            }
            else if (aoe_aiming == true)
            {
                aoe_aiming = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, real_position, Time.fixedDeltaTime * 15);
            transform.rotation = Quaternion.Lerp(transform.rotation, real_rotation, Time.fixedDeltaTime * 60);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            real_position = (Vector3)stream.ReceiveNext();
            real_rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}