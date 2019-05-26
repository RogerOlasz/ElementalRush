using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CrowdControlManager : MonoBehaviourPun
{
    Player my_player_script;
    Rigidbody my_rigidbody;

    private bool slippery_floor;

    private float no_cc_movement_speed;
    private float total_slow_percentage;

    // Start is called before the first frame update
    void Awake()
    {
        my_player_script = GetComponent<Player>();
        my_rigidbody = GetComponent<Rigidbody>();

        slippery_floor = false;
        total_slow_percentage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (slippery_floor)
        {
            my_rigidbody.AddForce(new Vector3(10, 0, 0));
        }
    }

    public void ApplySlipperyFloor()
    {
        slippery_floor = true;
    }

    public void RemoveSlipperyFloor()
    {
        slippery_floor = false;
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
