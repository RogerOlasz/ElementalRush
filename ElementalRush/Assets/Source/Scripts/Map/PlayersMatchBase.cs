using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayersMatchBase : MonoBehaviour
{
    BoxCollider base_trigger = null;
    MapManager map_manager = null;
    Player player = null;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetPhotonView().IsMine)
        {
            player = collider.gameObject.GetComponent<Player>();
            if (player.element_changer_script != null)
            {
                player.element_changer_script.OpenElementChangingMenu();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetPhotonView().IsMine)
        {
            player = collider.gameObject.GetComponent<Player>();
            if (player.element_changer_script != null)
            {
                player.element_changer_script.CloseElementChangingMenu();
            }
        }
    }

    //This is basically to show it updated with the map on the Unity Editor
    public void SetTriggerBase()
    {
        map_manager = GetComponentInParent<MapManager>();
        base_trigger = GetComponent<BoxCollider>();
        transform.position = new Vector3(map_manager.map_size.x / 2f, 0.75f, 0.5f);
        base_trigger.size = new Vector3(map_manager.map_size.x, 1.5f, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        base_trigger = GetComponent<BoxCollider>();
        map_manager = GetComponentInParent<MapManager>();

        transform.position = new Vector3(map_manager.map_size.x / 2f, 0.75f, 0.5f);
        base_trigger.size = new Vector3(map_manager.map_size.x, 1.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
