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
    float movement_speed;
    float item_carrying_speed;
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
                    movement_speed = bottled_fire.GetFireBaseSpeed();
                    item_carrying_speed = bottled_fire.GetFireItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Fire";
                    break;
                }
            case PlayerElementOnUse.Earth:
                {
                    movement_speed = bottled_earth.GetEarthBaseSpeed();
                    item_carrying_speed = bottled_earth.GetEarthItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Earth";
                    break;
                }
            case PlayerElementOnUse.Water:
                {
                    movement_speed = bottled_water.GetWaterBaseSpeed();
                    item_carrying_speed = bottled_water.GetWaterItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Water";
                    break;
                }
            case PlayerElementOnUse.Ice:
                {
                    movement_speed = bottled_ice.GetIceBaseSpeed();
                    item_carrying_speed = bottled_ice.GetIceItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Ice";
                    break;
                }
            case PlayerElementOnUse.Plant:
                {
                    movement_speed = bottled_plant.GetPlantBaseSpeed();
                    item_carrying_speed = bottled_plant.GetPlantItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Plant";
                    break;
                }
            case PlayerElementOnUse.Air:
                {
                    movement_speed = bottled_air.GetAirBaseSpeed();
                    item_carrying_speed = bottled_air.GetAirItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Air";
                    break;
                }
            case PlayerElementOnUse.Electric:
                {
                    movement_speed = bottled_electric.GetElectricBaseSpeed();
                    item_carrying_speed = bottled_electric.GetElectricItemCarryingSpeed();
                    element_text.GetComponent<TextMesh>().text = "Electric";
                    break;
                }
            case PlayerElementOnUse.Non_Element:
                {
                    //movement_speed = bottled_fire.GetFireBaseSpeed();
                    //item_carrying_speed = bottled_fire.GetFireItemCarryingSpeed();
                    break;
                }
            default:
                {
                    on_use_element = PlayerElementOnUse.Non_Element;
                    //movement_speed = bottled_fire.GetFireBaseSpeed();
                    //item_carrying_speed = bottled_fire.GetFireItemCarryingSpeed();
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

        SetPlayerStatsByElement(PlayerElementOnUse.Fire);
        Debug.Log(movement_speed);
    }

    void Update()
    {
        if(element_text != null)
        {
            element_text.transform.LookAt(Camera.main.transform.position);
            element_text.transform.Rotate(0, 180, 0);
        }
    }
}
