using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MhisoItem : MonoBehaviourPun
{
    PlayerItemManager player_item_manager_script;
    [SerializeField] private PlayerItemManager.Items my_type;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player_item_manager_script = collider.GetComponent<PlayerItemManager>();
            if (player_item_manager_script.is_carrying == false)
            {
                player_item_manager_script.SetItemCarrying(my_type);
                player_item_manager_script.is_carrying = true;

                photonView.RPC("ItemTaken", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            
        }
    }

    [PunRPC]
    public void ItemTaken()
    {
        StartCoroutine("MhisoRespawn");
    }

    IEnumerator MhisoRespawn()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(3);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;

        StopCoroutine(MhisoRespawn());
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "Fountain_Mhiso_Orange")
        {
            my_type = PlayerItemManager.Items.Tier1_Orange;
        }
        else if (gameObject.name == "Fountain_Mhiso_Lime")
        {
            my_type = PlayerItemManager.Items.Tier1_Lime;
        }
        else if (gameObject.name == "Fountain_Mhiso_Fuchsia")
        {
            my_type = PlayerItemManager.Items.Tier1_Fuchsia;
        }
        else if (gameObject.name == "Fountain_Mhiso_Turquoise")
        {
            my_type = PlayerItemManager.Items.Tier1_Turquoise;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
