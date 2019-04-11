using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject element_change_panel; //TODO try to set it active/unactive to unlock the rest of their children

    GameObject fire_image;
    GameObject earth_image;
    GameObject water_image;
    GameObject ice_image;
    GameObject plant_image;
    GameObject air_image;
    GameObject electric_image;
        
    public void OpenElementChangingMenu()
    {
        element_change_panel.SetActive(true);

        fire_image.SetActive(true);
        earth_image.SetActive(true);
        water_image.SetActive(true);
        ice_image.SetActive(true);
        plant_image.SetActive(true);
        air_image.SetActive(true);
        electric_image.SetActive(true);
    }

    public void CloseElementChangingMenu()
    {
        element_change_panel.SetActive(false);

        fire_image.SetActive(false);
        earth_image.SetActive(false);
        water_image.SetActive(false);
        ice_image.SetActive(false);
        plant_image.SetActive(false);
        air_image.SetActive(false);
        electric_image.SetActive(false);
    }

    //TODO Create methods to be called on click button mode, they may be on any other script

    // Start is called before the first frame update
    void Awake()
    {
        element_change_panel = GameObject.Find("Canvas").transform.Find("ElementChangePanel").gameObject;

        fire_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("FireImage").gameObject;
        earth_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("EarthImage").gameObject;
        water_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("WaterImage").gameObject;
        ice_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("IceImage").gameObject;
        plant_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("PlantImage").gameObject;
        air_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("AirImage").gameObject;
        electric_image = GameObject.Find("Canvas").transform.Find("ElementChangePanel").Find("ElectricImage").gameObject;

        element_change_panel.SetActive(false);

        fire_image.SetActive(false);
        earth_image.SetActive(false);
        water_image.SetActive(false);
        ice_image.SetActive(false);
        plant_image.SetActive(false);
        air_image.SetActive(false);
        electric_image.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
