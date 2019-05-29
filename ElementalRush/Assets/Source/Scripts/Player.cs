using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPun, IPunObservable
{
    #region PlayerClassAttributes

    PlayerController p_controller = null;
    Camera player_camera;
    CrowdControlManager cc_manager;

    //Camera Attributes
    public Vector3 camera_offset;
    public float camera_speed;

    [Header("UI")]
    [HideInInspector] public GameObject player_panel = null;
    [HideInInspector] public PlayerPanel player_panel_script = null;
    [HideInInspector] public GameObject element_changer = null;
    [HideInInspector] public UIElementChanger element_changer_script = null;

    //Bottled elements are those that are able to refill on the base
    FirePlayer bottled_fire = null;
    EarthPlayer bottled_earth = null;
    WaterPlayer bottled_water = null;
    IcePlayer bottled_ice = null;
    PlantPlayer bottled_plant = null;
    AirPlayer bottled_air = null;
    ElectricPlayer bottled_electric = null;

    [Header("Player attributes")]
    public int max_element_energy = 100;
    public int current_element_energy;

    //Player attributes
    [HideInInspector] public float movement_speed;
    [HideInInspector] public float item_carrying_speed;

    [Header("Straight Attack attributes")]
    public int straight_attack_player_consumption; //This info will be readed from (Element)Player.cs

    [Header("AoE Attack attributes")]
    public int aoe_attack_player_consumption;

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

    [HideInInspector] public PlayerElementOnUse on_use_element;

    #endregion

    public void RefreshSpeedFactor()
    {
        p_controller.SetSpeedFactor(movement_speed);
    }

    #region SetPlayerStatsByElement

    public void SetPlayerStatsByElement(PlayerElementOnUse new_on_use_element)
    {
        if (photonView.IsMine)
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

                        player_panel_script.player_status.text = "Fire";
                        break;
                    }
                case PlayerElementOnUse.Earth:
                    {
                        bottled_earth.SetEarthBaseSpeed();
                        bottled_earth.SetEarthItemCarryingSpeed();

                        bottled_earth.SetEarthStraightConsumption();
                        bottled_earth.SetEarthAoEConsumption();

                        player_panel_script.player_status.text = "Earth";
                        break;
                    }
                case PlayerElementOnUse.Water:
                    {
                        bottled_water.SetWaterBaseSpeed();
                        bottled_water.SetWaterItemCarryingSpeed();

                        bottled_water.SetWaterStraightConsumption();
                        bottled_water.SetWaterAoEConsumption();

                        player_panel_script.player_status.text = "Water";
                        break;
                    }
                case PlayerElementOnUse.Ice:
                    {
                        bottled_ice.SetIceBaseSpeed();
                        bottled_ice.SetIceItemCarryingSpeed();

                        bottled_ice.SetIceStraightConsumption();
                        bottled_ice.SetIceAoEConsumption();

                        player_panel_script.player_status.text = "Ice";
                        break;
                    }
                case PlayerElementOnUse.Plant:
                    {
                        bottled_plant.SetPlantBaseSpeed();
                        bottled_plant.SetPlantItemCarryingSpeed();

                        bottled_plant.SetPlantStraightConsumption();
                        bottled_plant.SetPlantAoEConsumption();

                        player_panel_script.player_status.text = "Plant";
                        break;
                    }
                case PlayerElementOnUse.Air:
                    {
                        bottled_air.SetAirBaseSpeed();
                        bottled_air.SetAirItemCarryingSpeed();

                        bottled_air.SetAirStraightConsumption();
                        bottled_air.SetAirAoEConsumption();

                        player_panel_script.player_status.text = "Air";
                        break;
                    }
                case PlayerElementOnUse.Electric:
                    {
                        bottled_electric.SetElectricBaseSpeed();
                        bottled_electric.SetElectricItemCarryingSpeed();

                        bottled_electric.SetElectricStraightConsumption();
                        bottled_electric.SetElectricAoEConsumption();

                        player_panel_script.player_status.text = "Electric";
                        break;
                    }
                case PlayerElementOnUse.Non_Element:
                    {
                        //TODO: Set the Non-Element properties
                        movement_speed = 7.5f;
                        item_carrying_speed = 0;

                        player_panel_script.player_status.text = "Non-Element";
                        break;
                    }
                default:
                    {
                        SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);

                        break;
                    }
            }

            cc_manager.SetNoCCSpeed();
            p_controller.SetSpeedFactor(movement_speed);
        }
    }

    #endregion

    #region SetPlayersELEMENT

    public void SetPlayerFire()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Fire);
            RefillElementEnergy(max_element_energy);
                        
            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerEarth()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Earth);
            RefillElementEnergy(max_element_energy);

            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerWater()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Water);
            RefillElementEnergy(max_element_energy);

            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerIce()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Ice);
            RefillElementEnergy(max_element_energy);

            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerPlant()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Plant);
            RefillElementEnergy(max_element_energy);

            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerAir()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Air);
            RefillElementEnergy(max_element_energy);

            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerElectric()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Electric);
            RefillElementEnergy(max_element_energy);

            element_changer_script.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    #endregion

    #region EnergySettings

    public void ConsumeElementEnergy(int energy_consumed)
    {
        if (photonView.IsMine)
        {
            current_element_energy -= energy_consumed;            
            player_panel_script.player_element_energy.fillAmount = ((float)current_element_energy / max_element_energy);
        }
    }

    public void RefillElementEnergy(int energy_quantity)
    {
        if (photonView.IsMine)
        {
            if (energy_quantity <= max_element_energy)
            {
                current_element_energy = energy_quantity;
                player_panel_script.player_element_energy.fillAmount = ((float)current_element_energy / max_element_energy);
            }
        }
    }

    #endregion
    
    #region StraightAttackSystem

    public void StraightAimingScheme()
    {
        //TODO: Code to render in some way a preview of the attack
    }

    public void StraightAttackSystem()
    {
        if (current_element_energy >= straight_attack_player_consumption)
        {
            StraightAttack();
        }
        else if (current_element_energy > 0)
        {
            StraightAttack();
            if (current_element_energy < 0)
            {
                current_element_energy = 0;
                player_panel_script.player_element_energy.fillAmount = 0;
            }
        }
    }

    private void StraightAttack()
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
                    return;
                }
            default:
                {
                    //SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);
                    Debug.Log("Something went wrong! In default switch.");
                    break;
                }
        }

        ConsumeElementEnergy(straight_attack_player_consumption);
    }

    #endregion

    #region AoEAttackSystem

    public void AoEAimingScheme()
    {
        //TODO: Code to render in some way a preview of the attack
    }

    public void AoEAttackSystem()
    {
        if (current_element_energy >= aoe_attack_player_consumption)
        {
            AoEAttack();
        }
    }

    private void AoEAttack()
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
                    return;
                }
            default:
                {
                    //SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);
                    Debug.Log("Something went wrong! In default switch.");
                    break;
                }
        }

        ConsumeElementEnergy(aoe_attack_player_consumption);
    }

    #endregion

    void Start()
    {
        if (photonView.IsMine)
        {            
            player_camera = Camera.main;
            if (gameObject.layer == LayerMask.NameToLayer("TeamRed"))
            {
                player_camera.transform.Rotate(-65, 0, 0);
                player_camera.transform.Rotate(0, 180, 0);
                player_camera.transform.Rotate(65, 0, 0);

                camera_offset.z = Mathf.Abs(camera_offset.z);
            }
            cc_manager = GetComponent<CrowdControlManager>();            

            player_panel = PhotonNetwork.Instantiate("PlayerPanel", Vector3.zero, Quaternion.identity, 0);
            player_panel.GetPhotonView().Owner.TagObject = this.gameObject;
            player_panel_script = player_panel.GetComponent<PlayerPanel>();

            element_changer = PhotonNetwork.Instantiate("ElementChangePanel", Vector3.zero, Quaternion.identity, 0);
            element_changer.GetPhotonView().Owner.TagObject = this.gameObject;
            element_changer_script = element_changer.GetComponent<UIElementChanger>();

            p_controller = GetComponent<PlayerController>();

            bottled_fire = transform.GetComponent<FirePlayer>();
            bottled_earth = transform.GetComponent<EarthPlayer>();
            bottled_water = transform.GetComponent<WaterPlayer>();
            bottled_ice = transform.GetComponent<IcePlayer>();
            bottled_plant = transform.GetComponent<PlantPlayer>();
            bottled_air = transform.GetComponent<AirPlayer>();
            bottled_electric = transform.GetComponent<ElectricPlayer>();

            SetPlayerStatsByElement(PlayerElementOnUse.Non_Element);

            RefillElementEnergy(max_element_energy);
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {            
            Vector3 camera_pos = player_camera.transform.position;
            Vector3 player_pos = transform.position;
            player_camera.transform.position = Vector3.Lerp(camera_pos, new Vector3(player_pos.x, player_pos.y, player_pos.z) + camera_offset, camera_speed); // * Time.deltaTime On camera speed If smoothier cam is needed
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(current_element_energy);            
        }
        else if (stream.IsReading)
        {    
            current_element_energy = (int)stream.ReceiveNext();            
        }
    }
}