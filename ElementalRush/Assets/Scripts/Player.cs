using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    GameObject element_text = null;
    UIManager ui_manager = null;

    //Bottled elements are those that are able to refill on the base
    FirePlayer bottled_fire = null;
    EarthPlayer bottled_earth = null;
    WaterPlayer bottled_water = null;
    IcePlayer bottled_ice = null;
    PlantPlayer bottled_plant = null;
    AirPlayer bottled_air = null;
    ElectricPlayer bottled_electric = null;

    //Player attributes
    [HideInInspector] public float movement_speed;
    [HideInInspector] public float item_carrying_speed;
    PlayerController p_controller;

    //Player Straight attack
    public int element_energy; //it decreases when the player uses spells
    public int straight_attack_player_consumption; //this info will be readed from ElementPlayer.cs
    public int aoe_attack_player_consumption;
    public float recharging_duration_straight = 0.5f;
    private bool aim_straight = false;
    private bool charged_straight = true;
    private bool charging_straight = false;

    //Player AoE attack
    public float recharging_duration_aoe = 0.5f;
    private bool aim_aoe = false;
    private bool charged_aoe = true;
    private bool charging_aoe = false;



    public enum PlayerElementPassives
    {
        Fire = 0,
        Earth,
        Water,
        Ice,
        Plant,
        Air,
        Electric
    };

    PlayerElementPassives on_use_passives;

    public enum PlayerElementOnUse
    {
        Fire = 0,
        Earth,
        Water,
        Ice,
        Plant,
        Air,
        Electric,
        Non_Element
    };

    PlayerElementOnUse on_use_element;

    public void SetPlayerStatsByElement(PlayerElementOnUse new_on_use_element)
    {
        on_use_element = new_on_use_element;

        switch (on_use_element)
        {
            case PlayerElementOnUse.Fire:
                {
                    bottled_fire.SetFireBaseSpeed();
                    bottled_fire.SetFireItemCarryingSpeed();

                    bottled_fire.SetFireStraightConsumption();
                    bottled_fire.SetFireAoEConsumption();

                    element_text.GetComponent<TextMesh>().text = "Fire";
                    break;
                }
            case PlayerElementOnUse.Earth:
                {
                    bottled_earth.SetEarthBaseSpeed();
                    bottled_earth.SetEarthItemCarryingSpeed();

                    element_text.GetComponent<TextMesh>().text = "Earth";
                    break;
                }
            case PlayerElementOnUse.Water:
                {
                    bottled_water.SetWaterBaseSpeed();
                    bottled_water.SetWaterItemCarryingSpeed();

                    element_text.GetComponent<TextMesh>().text = "Water";
                    break;
                }
            case PlayerElementOnUse.Ice:
                {
                    bottled_ice.SetIceBaseSpeed();
                    bottled_ice.SetIceItemCarryingSpeed();

                    element_text.GetComponent<TextMesh>().text = "Ice";
                    break;
                }
            case PlayerElementOnUse.Plant:
                {
                    bottled_plant.SetPlantBaseSpeed();
                    bottled_plant.SetPlantItemCarryingSpeed();

                    element_text.GetComponent<TextMesh>().text = "Plant";
                    break;
                }
            case PlayerElementOnUse.Air:
                {
                    bottled_air.SetAirBaseSpeed();
                    bottled_air.SetAirItemCarryingSpeed();

                    element_text.GetComponent<TextMesh>().text = "Air";
                    break;
                }
            case PlayerElementOnUse.Electric:
                {
                    bottled_electric.SetElectricBaseSpeed();
                    bottled_electric.SetElectricItemCarryingSpeed();

                    element_text.GetComponent<TextMesh>().text = "Electric";
                    break;
                }
            case PlayerElementOnUse.Non_Element:
                {
                    //TODO: Set the Non-Element properties
                    movement_speed = 7.5f;
                    item_carrying_speed = 0;

                    element_text.GetComponent<TextMesh>().text = "Non-Element";
                    break;
                }
            default:
                {
                    SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);

                    break;
                }
        }

        p_controller.SetSpeedFactor(movement_speed);
    }

    public void SetPlayerFire()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Fire);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerEarth()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Earth);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerWater()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Water);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerIce()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Ice);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerPlant()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Plant);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerAir()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Air);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerElectric()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Electric);
        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    IEnumerator Recharge_aoe()
    {
        charging_aoe = true;
        yield return new WaitForSeconds(recharging_duration_aoe);
        charged_aoe = true;
        charging_aoe = false;
    }

    IEnumerator Recharge_straight()
    {
        charging_straight = true;
        yield return new WaitForSeconds(recharging_duration_straight);
        charged_straight = true;
        charging_straight = false;
    }

    public void StraightAiming()
    {
        if (charged_straight == true && element_energy >= straight_attack_player_consumption && p_controller.direction_r1_no_normal.magnitude > p_controller.sensibility)
        {
            //GenerateArea(); // l'àrea haurà de desepareixer quan la direcció baixa de 0.5
            p_controller.last_r1 = p_controller.direction_r1_no_normal.magnitude;
            aim_straight = true;
        }

        if (charged_straight == true && aim_straight == true && p_controller.last_r1 > p_controller.cancel_attack && p_controller.direction_r1_no_normal.magnitude == 0)
        {
            switch (on_use_element)
            {
                case PlayerElementOnUse.Fire:
                    {
                        bottled_fire.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Earth:
                    {
                        bottled_earth.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Water:
                    {
                        bottled_water.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Ice:
                    {
                        bottled_ice.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Plant:
                    {
                        bottled_plant.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Air:
                    {
                        bottled_air.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Electric:
                    {
                        bottled_electric.StraightAttack();
                        break;
                    }
                case PlayerElementOnUse.Non_Element:
                    {
                        Debug.Log("Cannot attack, you have no element.");
                        break;
                    }
                default:
                    {
                        SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);
                        break;
                    }
            }

            element_energy -= straight_attack_player_consumption;

            charged_straight = false;
            aim_straight = false;
        }
        else if (charged_straight == true && aim_straight == true && p_controller.last_r1 < p_controller.cancel_attack && p_controller.direction_r1_no_normal.magnitude == 0)
        {
            aim_straight = false;
        }

        if (charged_straight == false && element_energy >= straight_attack_player_consumption && charging_straight == false) //the counter will stop 0.1 seconds after be charged
        {
            StartCoroutine(Recharge_straight());//co-rutina per truajar charged
        }
    }


    public void AoEAiming()
    {
        if (charged_aoe == true && element_energy >= straight_attack_player_consumption && p_controller.direction_r1_no_normal.magnitude > p_controller.sensibility)
        {
            //GenerateArea(); // l'àrea haurà de desepareixer quan la direcció baixa de 0.5
            p_controller.last_r2 = p_controller.direction_r1_no_normal.magnitude;
            aim_aoe = true;
        }

        if (charged_aoe == true && aim_aoe == true && p_controller.last_r2 > p_controller.cancel_attack && p_controller.direction_r1_no_normal.magnitude == 0)
        {
            switch (on_use_element)
            {
                case PlayerElementOnUse.Fire:
                    {
                        bottled_fire.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Earth:
                    {
                        bottled_earth.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Water:
                    {
                        bottled_water.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Ice:
                    {
                        bottled_ice.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Plant:
                    {
                        bottled_plant.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Air:
                    {
                        bottled_air.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Electric:
                    {
                        bottled_electric.AoEAttack();
                        break;
                    }
                case PlayerElementOnUse.Non_Element:
                    {
                        Debug.Log("Cannot attack, you have no element.");
                        break;
                    }
                default:
                    {
                        SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);
                        break;
                    }
            }

            element_energy -= aoe_attack_player_consumption;

            charged_aoe = false;
            aim_aoe = false;
        }
        else if (charged_aoe == true && aim_aoe == true && p_controller.last_r2 < p_controller.cancel_attack && p_controller.direction_r1_no_normal.magnitude == 0)
        {
            aim_aoe = false;
        }

        if (charged_aoe == false && element_energy >= straight_attack_player_consumption && charging_aoe == false) //the counter will stop 0.1 seconds after be charged
        {
            StartCoroutine(Recharge_aoe());//co-rutina per truajar charged
        }
    }



    void Start()
    {
        element_text = GameObject.Find("PlayerElementText").gameObject;
        ui_manager = GameObject.Find("UIManager").GetComponent<UIManager>();

        p_controller = GetComponent<PlayerController>();

        bottled_fire = GetComponent<FirePlayer>();
        bottled_earth = GetComponent<EarthPlayer>();
        bottled_water = GetComponent<WaterPlayer>();
        bottled_ice = GetComponent<IcePlayer>();
        bottled_plant = GetComponent<PlantPlayer>();
        bottled_air = GetComponent<AirPlayer>();
        bottled_electric = GetComponent<ElectricPlayer>();

        element_energy = 100;

        SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);

        if (element_text != null)
        {
            element_text.transform.LookAt(Camera.main.transform.position);
            element_text.transform.Rotate(0, 180, 0);
        }
    }

    void Update()
    {

        StraightAiming();
        if (element_text != null)
        {
            element_text.transform.LookAt(Camera.main.transform.position);
            element_text.transform.Rotate(0, 180, 0);
        }
    }
}
