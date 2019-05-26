using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CrowdControlManager : MonoBehaviourPun
{
    Player my_player_script;
    PlayerController my_p_controller;
    Rigidbody my_rigidbody;

    private bool slippery_floor;
    public float slippery_force;

    private bool windy_tunnel;
    public float wind_force;
    public Vector3 wind_direction;

    private float no_cc_movement_speed;
    private float total_slow_percentage;

    // Start is called before the first frame update
    void Awake()
    {
        my_player_script = GetComponent<Player>();
        my_rigidbody = GetComponent<Rigidbody>();
        my_p_controller = GetComponent<PlayerController>();

        slippery_floor = false;
        windy_tunnel = false;
        total_slow_percentage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (slippery_floor)
        {
            Vector3 direction = my_rigidbody.velocity.normalized;
            my_rigidbody.AddForce(new Vector3(slippery_force * direction.x, 0, slippery_force * direction.z));
        }

        if(windy_tunnel)
        {
            my_rigidbody.AddForce(new Vector3((wind_force) * wind_direction.x, 0, (wind_force) * wind_direction.z));
        }
    }

    public void ApplyWindyTunnel(float _wind_force, Vector3 _wind_direction)
    {
        wind_force = _wind_force;
        wind_direction = _wind_direction;
        windy_tunnel = true;
        my_p_controller.velocity_control = true;
    }

    public void RemoveWindyTunnel()
    {
        windy_tunnel = false;
        my_p_controller.velocity_control = false;
    }

    public void ApplySlipperyFloor(float _slippery_force)
    {
        slippery_force = _slippery_force;
        slippery_floor = true;
        my_p_controller.velocity_control = true;
    }

    public void RemoveSlipperyFloor()
    {
        slippery_floor = false;
        my_p_controller.velocity_control = false;
    }

    public void EraseElementEnergy(int energy_to_erase)
    {
        if (my_player_script.on_use_element != Player.PlayerElementOnUse.Non_Element)
        {
            if (photonView.IsMine)
            {
                if(energy_to_erase < my_player_script.current_element_energy && my_player_script.current_element_energy >= energy_to_erase) //This last check should be on player script
                {
                    my_player_script.ConsumeElementEnergy(energy_to_erase);
                }
            }
        }
    }

    public void SetNoCCSpeed()
    {
        no_cc_movement_speed = my_player_script.movement_speed;
    }

    public void ApplySlowCC(float slow_percentage, bool is_stackable = true)
    {        
        if (my_player_script.on_use_element != Player.PlayerElementOnUse.Non_Element)
        {
            if (photonView.IsMine)
            {
                float new_movement_speed = -1;

                if (slow_percentage < 1f && slow_percentage > 0f)
                {
                    if (is_stackable)
                    {
                        total_slow_percentage += slow_percentage;

                        if (total_slow_percentage < 1f && total_slow_percentage > 0f)
                        {
                            new_movement_speed = no_cc_movement_speed - (no_cc_movement_speed * total_slow_percentage);
                        }
                        else
                        {
                            //TODO maybe transform it in a root?
                            Debug.LogWarning("Total slow percentage is in trouble: " + total_slow_percentage);
                            return;
                        }
                    }
                    else
                    {
                        if (total_slow_percentage < slow_percentage)
                        {
                            new_movement_speed = (no_cc_movement_speed * slow_percentage);
                        }
                        else
                        {
                            Debug.LogWarning("The unstackable slow could'nt be applied. Slow percentage SENT:" + slow_percentage);
                            return;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Slow percentage SENT is too high or too low! Slow percentage SENT: " + slow_percentage);
                    return;
                }

                if (new_movement_speed > 0)
                {
                    my_player_script.movement_speed = new_movement_speed;
                    my_player_script.RefreshSpeedFactor();
                    Debug.Log(my_player_script.movement_speed);
                }
                else
                {
                    Debug.LogWarning("Something went wrong." + new_movement_speed);
                    return;
                }
            }
        }
    }

    public void RemoveSlowCC(float slow_amount_to_remove, bool was_stackable = true)
    {
        if (my_player_script.on_use_element != Player.PlayerElementOnUse.Non_Element)
        {
            if (photonView.IsMine)
            {
                float new_movement_speed = -1;

                if (slow_amount_to_remove < 1f && slow_amount_to_remove > 0f)
                {
                    if (was_stackable)
                    {
                        if (total_slow_percentage > slow_amount_to_remove || Mathf.Approximately(total_slow_percentage, slow_amount_to_remove))
                        {
                            total_slow_percentage -= slow_amount_to_remove;

                            new_movement_speed = no_cc_movement_speed - (no_cc_movement_speed * total_slow_percentage);
                        }
                        else
                        {
                            Debug.LogWarning("Slow stack: " + total_slow_percentage.ToString());
                            Debug.LogWarning("Too much slow to remove from the stack. Slow to remove: " + slow_amount_to_remove.ToString());
                            return;
                        }
                    }
                    else
                    {
                        if ((no_cc_movement_speed - my_player_script.movement_speed) > 0f)
                        {
                            float total_reduced_speed_percentage = (((no_cc_movement_speed - my_player_script.movement_speed) / no_cc_movement_speed) * 100);

                            if (slow_amount_to_remove < total_reduced_speed_percentage)
                            {
                                new_movement_speed = no_cc_movement_speed - (no_cc_movement_speed * (total_reduced_speed_percentage - slow_amount_to_remove));
                            }
                            else
                            {
                                Debug.LogWarning("The slow amount to remove is bigger than the actual slow on player.");
                                return;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("There is no slow to remove.");
                            return;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Slow percentage to remove is too high or too low! Slow percentage to remove SENT: " + slow_amount_to_remove);
                    return;
                }

                if (new_movement_speed > 0)
                {
                    my_player_script.movement_speed = new_movement_speed;
                    my_player_script.RefreshSpeedFactor();
                    Debug.Log(my_player_script.movement_speed);
                }
                else
                {
                    Debug.LogWarning("Something went wrong." + new_movement_speed);
                    return;
                }
            }
        }
    }

    public void RestoreSlowCC()
    {
        total_slow_percentage = 0;

        my_player_script.movement_speed = no_cc_movement_speed;
    }
}
