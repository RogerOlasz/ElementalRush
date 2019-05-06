using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementChanger : MonoBehaviour
{
    Player player;

    [HideInInspector] public Button fire_button;
    [HideInInspector] public Button earth_button;
    [HideInInspector] public Button water_button;
    [HideInInspector] public Button ice_button;
    [HideInInspector] public Button plant_button;
    [HideInInspector] public Button air_button;
    [HideInInspector] public Button electric_button;
        
    public void OpenElementChangingMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseElementChangingMenu()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        
        fire_button = GameObject.Find("FireButton").GetComponent<Button>();
        earth_button = GameObject.Find("EarthButton").GetComponent<Button>();
        water_button = GameObject.Find("WaterButton").GetComponent<Button>();
        ice_button = GameObject.Find("IceButton").GetComponent<Button>();
        plant_button = GameObject.Find("PlantButton").GetComponent<Button>();
        air_button = GameObject.Find("AirButton").GetComponent<Button>();
        electric_button = GameObject.Find("ElectricButton").GetComponent<Button>();

        fire_button.onClick.AddListener(delegate () { player.SetPlayerFire(); });
        earth_button.onClick.AddListener(delegate () { player.SetPlayerEarth(); });
        water_button.onClick.AddListener(delegate () { player.SetPlayerWater(); });
        ice_button.onClick.AddListener(delegate () { player.SetPlayerIce(); });
        plant_button.onClick.AddListener(delegate () { player.SetPlayerPlant(); });
        air_button.onClick.AddListener(delegate () { player.SetPlayerAir(); });
        electric_button.onClick.AddListener(delegate () { player.SetPlayerElectric(); });

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
