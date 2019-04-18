using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMatchBase : MonoBehaviour
{
    UIManager ui_manager = null;
    BoxCollider base_trigger = null;
    MapManager map_manager = null;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (ui_manager != null)
            {
                ui_manager.OpenElementChangingMenu(); //DEPRACATED TODO Multiple tile collision are not able to give a good experience over element changer menu
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (ui_manager != null)
            {
                ui_manager.CloseElementChangingMenu();
            }
        }
    }

    public void SetTriggerBase()
    {
        if(map_manager == null)
        {
            map_manager = GetComponentInParent<MapManager>();
        }
        else if (base_trigger == null)
        {
            base_trigger = GetComponent<BoxCollider>();
        }
        transform.position = new Vector3(map_manager.map_size.x / 2f, 0.75f, 0.5f);
        base_trigger.size = new Vector3(map_manager.map_size.x, 1.5f, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        ui_manager = GameObject.Find("UIManager").GetComponent<UIManager>();
        base_trigger = GetComponent<BoxCollider>();
        map_manager = GetComponentInParent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
