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

    //------ Player attack atributes ------
    public int max_element_energy = 100;
    public int current_element_energy;
    private int extra_attack = 0;

    //Player Straight attack
    public int straight_attack_player_consumption; //This info will be readed from (Element)Player.cs
    public float shooting_rate_duration_straight = 0.5f;
    private bool aim_straight = false;
    private bool shoot_rate_straight = true;
    private bool shooting_rate_straight = false;

    //Player AoE attack
    public int aoe_attack_player_consumption;
    public float shooting_rate_duration_aoe = 0.5f;
    private bool aim_aoe = false;
    private bool shoot_rate_aoe = true;
    private bool shooting_rate_aoe = false;

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
        current_element_energy = max_element_energy;

        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerEarth()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Earth);
        current_element_energy = max_element_energy;

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
        current_element_energy = max_element_energy;

        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerPlant()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Plant);
        current_element_energy = max_element_energy;

        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerAir()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Air);
        current_element_energy = max_element_energy;

        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    public void SetPlayerElectric()
    {
        SetPlayerStatsByElement(PlayerElementOnUse.Electric);
        current_element_energy = max_element_energy;

        ui_manager.CloseElementChangingMenu();
        Debug.Log(movement_speed);
    }

    IEnumerator Recharge_straight()
    {
        shooting_rate_straight = true;
        yield return new WaitForSeconds(shooting_rate_duration_straight);
        shoot_rate_straight = true;
        shooting_rate_straight = false;
    }

    IEnumerator Recharge_aoe()
    {
        shooting_rate_aoe = true;
        yield return new WaitForSeconds(shooting_rate_duration_aoe);
        shoot_rate_aoe = true;
        shooting_rate_aoe = false;
    }

    public void StraightAiming()
    {
        if (shoot_rate_straight == true && current_element_energy >= straight_attack_player_consumption && p_controller.direction_r1_no_normal.magnitude > p_controller.sensibility)
        {
            //GenerateArea(); // l'àrea haurà de desepareixer quan la direcció baixa de 0.5
            p_controller.last_r1 = p_controller.direction_r1_no_normal.magnitude;
            aim_straight = true;
        }
        else if (shoot_rate_straight == true && extra_attack > 0 && p_controller.direction_r1_no_normal.magnitude > p_controller.sensibility)
        {
            //GenerateArea(); // l'àrea haurà de desepareixer quan la direcció baixa de 0.5
            p_controller.last_r1 = p_controller.direction_r1_no_normal.magnitude;
            aim_straight = true;
        }

        if (shoot_rate_straight == true && aim_straight == true && p_controller.last_r1 > p_controller.cancel_attack_r1 && p_controller.direction_r1_no_normal.magnitude == 0 && current_element_energy > straight_attack_player_consumption)
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

            current_element_energy -= straight_attack_player_consumption;

            if (current_element_energy < straight_attack_player_consumption)
            {
                extra_attack += 1;
            }

            shoot_rate_straight = false;
            aim_straight = false;
        }
        else if(shoot_rate_straight == true && aim_straight == true && p_controller.last_r1 > p_controller.cancel_attack_r1 && p_controller.direction_r1_no_normal.magnitude == 0 && extra_attack > 0)
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

            current_element_energy = 0;
            extra_attack -= 1;
        }
        else if (shoot_rate_straight == true && aim_straight == true && p_controller.last_r1 < p_controller.cancel_attack_r1 && p_controller.direction_r1_no_normal.magnitude == 0)
        {
            aim_straight = false;
        }

        if (shoot_rate_straight == false && current_element_energy > straight_attack_player_consumption && shooting_rate_straight == false) //the counter will stop 0.1 seconds after be charged
        {
            StartCoroutine(Recharge_straight());//co-rutina per truajar charged
        }
        else if (shoot_rate_straight == false && extra_attack > 0 && shooting_rate_straight == false)
        {
            StartCoroutine(Recharge_straight());
        }
    }

    public void AoEAiming()
    {
        if (shoot_rate_aoe == true && current_element_energy >= straight_attack_player_consumption && p_controller.direction_r2_no_normal.magnitude > p_controller.sensibility)
        {
            //GenerateArea(); // l'àrea haurà de desepareixer quan la direcció baixa de 0.5
            p_controller.last_r2 = p_controller.direction_r2_no_normal.magnitude;
            aim_aoe = true;
        }

        if (shoot_rate_aoe == true && aim_aoe == true && p_controller.last_r2 > p_controller.cancel_attack_r2 && p_controller.direction_r2_no_normal.magnitude == 0)
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

            current_element_energy -= aoe_attack_player_consumption;

            if (current_element_energy < straight_attack_player_consumption)
            {
                extra_attack += 1;
            }

            shoot_rate_aoe = false;
            aim_aoe = false;
        }
        else if (shoot_rate_aoe == true && aim_aoe == true && p_controller.last_r2 < p_controller.cancel_attack_r2 && p_controller.direction_r2_no_normal.magnitude == 0)
        {
            aim_aoe = false;
        }

        if (shoot_rate_aoe == false && current_element_energy >= straight_attack_player_consumption && shooting_rate_aoe == false) //the counter will stop 0.1 seconds after be charged
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

        SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);

        current_element_energy = max_element_energy;

        if (element_text != null)
        {
            element_text.transform.LookAt(Camera.main.transform.position);
            element_text.transform.Rotate(0, 180, 0);
        }
    }

    void Update()
    {
        if (element_text != null)
        {
            element_text.transform.LookAt(Camera.main.transform.position);
            element_text.transform.Rotate(0, 180, 0);
        }
    }
}
