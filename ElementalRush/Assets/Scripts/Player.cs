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
    public int element_energy;

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
        if (element_text != null)
        {
            element_text.transform.LookAt(Camera.main.transform.position);
            element_text.transform.Rotate(0, 180, 0);
        }
    }
}
