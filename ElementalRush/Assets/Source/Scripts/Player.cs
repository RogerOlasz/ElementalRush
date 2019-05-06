using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPun, IPunObservable
{
    #region PlayerClassAttributes

    UIElementChanger ui_manager = null;
    PlayerController p_controller = null;

    //Camera Attributes
    public Vector3 camera_offset;
    public float camera_speed;
    private Quaternion canvas_rotation;
    private Vector3 canvas_position;

    [Header("UI")]
    GameObject player_canvas = null;
    public Image element_energy_bar;
    GameObject element_text = null;

    //Bottled elements are those that are able to refill on the base
    FirePlayer bottled_fire = null;
    EarthPlayer bottled_earth = null;
    WaterPlayer bottled_water = null;
    IcePlayer bottled_ice = null;
    PlantPlayer bottled_plant = null;
    AirPlayer bottled_air = null;
    ElectricPlayer bottled_electric = null;

    [Header("Player attributes")]
    //------ Player attack atributes ------
    public int max_element_energy = 100;
    public int current_element_energy;
    private int extra_attack = 0;

    //Player attributes
    [HideInInspector] public float movement_speed;
    [HideInInspector] public float item_carrying_speed;

    [Header("Straight Attack attributes")]
    //Player Straight attack
    public int straight_attack_player_consumption; //This info will be readed from (Element)Player.cs
    public float shooting_rate_duration_straight = 0.5f;
    private bool aim_straight = false;
    private bool shoot_rate_straight = true;
    private bool shooting_rate_straight = false;

    [Header("AoE Attack attributes")]
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

    #endregion

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

                        element_text.GetComponent<Text>().text = "Fire";
                        break;
                    }
                case PlayerElementOnUse.Earth:
                    {
                        bottled_earth.SetEarthBaseSpeed();
                        bottled_earth.SetEarthItemCarryingSpeed();

                        bottled_earth.SetEarthStraightConsumption();
                        bottled_earth.SetEarthAoEConsumption();

                        element_text.GetComponent<Text>().text = "Earth";
                        break;
                    }
                case PlayerElementOnUse.Water:
                    {
                        bottled_water.SetWaterBaseSpeed();
                        bottled_water.SetWaterItemCarryingSpeed();

                        bottled_water.SetWaterStraightConsumption();
                        bottled_water.SetWaterAoEConsumption();

                        element_text.GetComponent<Text>().text = "Water";
                        break;
                    }
                case PlayerElementOnUse.Ice:
                    {
                        bottled_ice.SetIceBaseSpeed();
                        bottled_ice.SetIceItemCarryingSpeed();

                        bottled_ice.SetIceStraightConsumption();
                        bottled_ice.SetIceAoEConsumption();

                        element_text.GetComponent<Text>().text = "Ice";
                        break;
                    }
                case PlayerElementOnUse.Plant:
                    {
                        bottled_plant.SetPlantBaseSpeed();
                        bottled_plant.SetPlantItemCarryingSpeed();

                        bottled_plant.SetPlantStraightConsumption();
                        bottled_plant.SetPlantAoEConsumption();

                        element_text.GetComponent<Text>().text = "Plant";
                        break;
                    }
                case PlayerElementOnUse.Air:
                    {
                        bottled_air.SetAirBaseSpeed();
                        bottled_air.SetAirItemCarryingSpeed();

                        bottled_air.SetAirStraightConsumption();
                        bottled_air.SetAirAoEConsumption();

                        element_text.GetComponent<Text>().text = "Air";
                        break;
                    }
                case PlayerElementOnUse.Electric:
                    {
                        bottled_electric.SetElectricBaseSpeed();
                        bottled_electric.SetElectricItemCarryingSpeed();

                        bottled_electric.SetElectricStraightConsumption();
                        bottled_electric.SetElectricAoEConsumption();

                        element_text.GetComponent<Text>().text = "Electric";
                        break;
                    }
                case PlayerElementOnUse.Non_Element:
                    {
                        //TODO: Set the Non-Element properties
                        movement_speed = 7.5f;
                        item_carrying_speed = 0;

                        element_text.GetComponent<Text>().text = "Non-Element";
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
    }

    #endregion

    #region SetPlayersELEMENT

    public void SetPlayerFire()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Fire);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerEarth()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Earth);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerWater()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Water);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerIce()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Ice);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerPlant()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Plant);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerAir()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Air);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
            Debug.Log(movement_speed);
        }
    }

    public void SetPlayerElectric()
    {
        if (photonView.IsMine)
        {
            SetPlayerStatsByElement(PlayerElementOnUse.Electric);
            RefillElementEnergy(max_element_energy);

            ui_manager.CloseElementChangingMenu();
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
            element_energy_bar.fillAmount = ((float)current_element_energy / max_element_energy);
        }
    }

    public void RefillElementEnergy(int energy_quantity)
    {
        if (photonView.IsMine)
        {
            current_element_energy = energy_quantity;
            element_energy_bar.fillAmount = ((float)current_element_energy / max_element_energy);
        }
    }

    #endregion

    #region FireRateSystem

    IEnumerator Recharge_straight()
    {
        if (photonView.IsMine)
        {
            shooting_rate_straight = true;
            yield return new WaitForSeconds(shooting_rate_duration_straight);
            shoot_rate_straight = true;
            shooting_rate_straight = false;
        }
    }

    IEnumerator Recharge_aoe()
    {
        if (photonView.IsMine)
        {
            shooting_rate_aoe = true;
            yield return new WaitForSeconds(shooting_rate_duration_aoe);
            shoot_rate_aoe = true;
            shooting_rate_aoe = false;
        }
    }

    #endregion

    #region StraightAttackSystem

    public void StraightAiming()
    {
        if (photonView.IsMine)
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

                ConsumeElementEnergy(straight_attack_player_consumption);

                if (current_element_energy < straight_attack_player_consumption)
                {
                    extra_attack += 1; //TODO Still need fixes, does not work properly
                }

                shoot_rate_straight = false;
                aim_straight = false;
            }
            else if (shoot_rate_straight == true && aim_straight == true && p_controller.last_r1 > p_controller.cancel_attack_r1 && p_controller.direction_r1_no_normal.magnitude == 0 && extra_attack > 0)
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

                ConsumeElementEnergy(current_element_energy);
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
    }

    #endregion

    #region AoEAttackSystem

    public void AoEAiming()
    {
        if (photonView.IsMine)
        {
            if (shoot_rate_aoe == true && current_element_energy >= aoe_attack_player_consumption && p_controller.direction_r2_no_normal.magnitude > p_controller.sensibility)
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

                ConsumeElementEnergy(aoe_attack_player_consumption);

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

            if (shoot_rate_aoe == false && current_element_energy >= aoe_attack_player_consumption && shooting_rate_aoe == false) //the counter will stop 0.1 seconds after be charged
            {
                StartCoroutine(Recharge_aoe());//co-rutina per truajar charged
            }
        }
    }

    #endregion

    void Start()
    {
        if (photonView.IsMine)
        {            
            ui_manager = GameObject.Find("Canvas").transform.Find("ElementChangePanel").GetComponent<UIElementChanger>();
            player_canvas = transform.Find("PlayerCanvas").gameObject;
            element_text = player_canvas.transform.Find("PlayerElementText").gameObject;

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

            player_canvas.transform.Rotate(45, 0, 0);
            canvas_rotation = player_canvas.transform.rotation;
            canvas_position = transform.position - player_canvas.transform.position;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            player_canvas.transform.rotation = canvas_rotation;
            player_canvas.transform.position = transform.position - canvas_position;
        }
        else
        {
            player_canvas.transform.rotation = canvas_rotation;
        }
    }

    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            //TODO Camera must be fixed
            Vector3 camera_pos = Camera.main.gameObject.transform.position;
            Vector3 player_pos = transform.position;
            Camera.main.transform.position = Vector3.Lerp(camera_pos, new Vector3(player_pos.x, player_pos.y, player_pos.z) + camera_offset, camera_speed); // * Time.deltaTime On camera speed If smoothier cam is needed
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(canvas_rotation);
            //stream.SendNext(health);
            //stream.SendNext(username);
        }
        else if (stream.IsReading)
        {
            //health = (float)stream.ReceiveNext();
            //username = (string)stream.ReceiveNext();
            //user_text.text = username;
            canvas_rotation = (Quaternion)stream.ReceiveNext();       
        }
    }
}
