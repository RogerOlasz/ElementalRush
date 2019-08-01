using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CraftingMachineProcessor_1 : MonoBehaviourPun, IPunObservable
{    
    PlayerItemManager player_item_manager_1 = null;
    PlayerItemManager player_item_manager_2 = null;

    public Image crafting_slot_1_image;
    public Image crafting_slot_2_image;

    Color orange;
    Color fuchsia;
    Color turquoise;
    Color lime;

    private bool adding_slot_1 = false;
    private bool adding_slot_2 = false;

    private float timer_1 = 0;
    private float timer_2 = 0;

    PlayerItemManager.Items slot_1 = PlayerItemManager.Items.None;
    PlayerItemManager.Items slot_2 = PlayerItemManager.Items.None;

    private bool crafting_item = false;

    public float material_insert_duration = 1f;
    public float item_crafting_duration = 2f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") // TODO have to check the layers
        {
            if (player_item_manager_1 == null && slot_1 == PlayerItemManager.Items.None)
            {
                if (gameObject.layer == 0 || gameObject.layer == collider.gameObject.layer)
                {
                    player_item_manager_1 = collider.GetComponent<PlayerItemManager>();

                    if (player_item_manager_1.GetItem() != PlayerItemManager.Items.None)
                    {
                        adding_slot_1 = true;
                    }
                }
            }
            else if (player_item_manager_2 == null && slot_2 == PlayerItemManager.Items.None)
            {
                if (gameObject.layer == 0 || gameObject.layer == collider.gameObject.layer)
                {
                    player_item_manager_2 = collider.GetComponent<PlayerItemManager>();

                    if (player_item_manager_2.GetItem() != PlayerItemManager.Items.None)
                    {
                        adding_slot_2 = true;
                    }
                }
            }            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (player_item_manager_1 == collider.GetComponent<PlayerItemManager>())
            {
                player_item_manager_1 = null;
                adding_slot_1 = false;
            }
            else if (player_item_manager_2 == collider.GetComponent<PlayerItemManager>())
            {
                player_item_manager_2 = null;
                adding_slot_2 = false;
            }
        }
    }

    IEnumerator CraftingItem()
    {
        yield return new WaitForSeconds(item_crafting_duration);
        GetComponent<CraftingMachineRPG_1>().TimeToCraft(slot_1, slot_2);

        ResetMachine();

        this.enabled = false;
    }

    public void ResetMachine()
    {
        crafting_slot_1_image.fillAmount = 0;
        crafting_slot_2_image.fillAmount = 0;

        timer_1 = 0;
        timer_2 = 0;

        slot_1 = PlayerItemManager.Items.None;
        slot_2 = PlayerItemManager.Items.None;

        crafting_item = false;

        adding_slot_1 = false;
        adding_slot_2 = false;

        player_item_manager_1 = null;
        player_item_manager_2 = null;

        crafting_slot_1_image.color = Color.white;
        crafting_slot_2_image.color = Color.white;

        this.gameObject.layer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetMachine();

        orange = new Vector4(1f, 96f / 255f, 0f, 1f);
        fuchsia = new Vector4(1f, 0f, 109f / 255f, 1f);
        turquoise = new Vector4(0f, 230f / 255f, 190f / 255f, 1f);
        lime = new Vector4(188f / 255f, 1f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(slot_1 != PlayerItemManager.Items.None && slot_2 != PlayerItemManager.Items.None && crafting_item == false)
        {
            StartCoroutine(CraftingItem());
            crafting_item = true;
        }

        if (slot_1 == PlayerItemManager.Items.None)
        {
            if (adding_slot_1)
            {
                timer_1 += Time.deltaTime;
                crafting_slot_1_image.fillAmount = timer_1;

                if (timer_1 >= material_insert_duration)
                {
                    slot_1 = player_item_manager_1.GetItem();
                    player_item_manager_1.RemoveItem();

                    if (gameObject.layer == 0)
                    {
                        gameObject.layer = player_item_manager_1.gameObject.layer;
                    }

                    switch (slot_1)
                    {
                        case PlayerItemManager.Items.Tier1_Fuchsia:
                            {
                                crafting_slot_1_image.color = fuchsia;
                                break;
                            }
                        case PlayerItemManager.Items.Tier1_Lime:
                            {
                                crafting_slot_1_image.color = lime;
                                break;
                            }
                        case PlayerItemManager.Items.Tier1_Orange:
                            {
                                crafting_slot_1_image.color = orange;
                                break;
                            }
                        case PlayerItemManager.Items.Tier1_Turquoise:
                            {
                                crafting_slot_1_image.color = turquoise;
                                break;
                            }
                    }
                }
            }
            else
            {
                crafting_slot_1_image.fillAmount = 0;
                timer_1 = 0;
            }
        }

        if (slot_2 == PlayerItemManager.Items.None)
        {
            if (adding_slot_2)
            {
                if (player_item_manager_2.GetItem() != slot_1 || slot_1 == PlayerItemManager.Items.None)
                {
                    timer_2 += Time.deltaTime;
                    crafting_slot_2_image.fillAmount = timer_2;

                    if (timer_2 >= material_insert_duration)
                    {
                        slot_2 = player_item_manager_2.GetItem();
                        player_item_manager_2.RemoveItem();

                        if (gameObject.layer == 0)
                        {
                            gameObject.layer = player_item_manager_2.gameObject.layer;
                        }

                        switch (slot_2)
                        {
                            case PlayerItemManager.Items.Tier1_Fuchsia:
                                {
                                    crafting_slot_2_image.color = fuchsia;
                                    break;
                                }
                            case PlayerItemManager.Items.Tier1_Lime:
                                {
                                    crafting_slot_2_image.color = lime;
                                    break;
                                }
                            case PlayerItemManager.Items.Tier1_Orange:
                                {
                                    crafting_slot_2_image.color = orange;
                                    break;
                                }
                            case PlayerItemManager.Items.Tier1_Turquoise:
                                {
                                    crafting_slot_2_image.color = turquoise;
                                    break;
                                }
                        }
                    }
                }
            }
            else
            {
                crafting_slot_2_image.fillAmount = 0;
                timer_2 = 0;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(crafting_slot_1_image.fillAmount);
            stream.SendNext(crafting_slot_2_image.fillAmount);
        }
        else if (stream.IsReading)
        {
            crafting_slot_1_image.fillAmount = (float)stream.ReceiveNext();
            crafting_slot_2_image.fillAmount = (float)stream.ReceiveNext();
        }
    }
}
