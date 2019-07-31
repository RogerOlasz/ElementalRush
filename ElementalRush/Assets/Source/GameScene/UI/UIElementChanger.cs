using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UIElementChanger : MonoBehaviourPun, IPunObservable
{
    GameObject my_player;
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
        if (photonView.IsMine)
        {
            gameObject.SetActive(true);
        }
    }

    public void CloseElementChangingMenu()
    {
        if (photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        if (photonView.IsMine)
        {
            my_player = (GameObject)photonView.Owner.TagObject;
            player = my_player.GetComponent<Player>();

            fire_button = transform.Find("FireButton").GetComponent<Button>();
            earth_button = transform.Find("EarthButton").GetComponent<Button>();
            water_button = transform.Find("WaterButton").GetComponent<Button>();
            ice_button = transform.Find("IceButton").GetComponent<Button>();
            plant_button = transform.Find("PlantButton").GetComponent<Button>();
            air_button = transform.Find("AirButton").GetComponent<Button>();
            electric_button = transform.Find("ElectricButton").GetComponent<Button>();

            //Setting up OnClick methods
            fire_button.onClick.AddListener(delegate () { player.SetPlayerFire(); });
            earth_button.onClick.AddListener(delegate () { player.SetPlayerEarth(); });
            water_button.onClick.AddListener(delegate () { player.SetPlayerWater(); });
            ice_button.onClick.AddListener(delegate () { player.SetPlayerIce(); });
            plant_button.onClick.AddListener(delegate () { player.SetPlayerPlant(); });
            air_button.onClick.AddListener(delegate () { player.SetPlayerAir(); });
            electric_button.onClick.AddListener(delegate () { player.SetPlayerElectric(); });            
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
