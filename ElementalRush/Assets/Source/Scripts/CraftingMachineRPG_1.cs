using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMachineRPG_1 : MonoBehaviour
{
    public GameObject game_manager;

    [SerializeField] private int crafting_machine_level = 0;
    [SerializeField] private int crafting_machine_points = 0;
    [SerializeField] private float machine_quality = 0;
    [SerializeField] private int items_crafted = 0;
    [SerializeField] private int total_crafting_points = 0;

    private bool ready_to_infuse = false;

    [Header("Machine Attributes")]
    public int points_to_level_up;

    public int tier_1_quality_points;
    public int tier_2_quality_points;

    public int level_up_difficulty;

    public int energy_needed_to_infuse;

    [Header("Cooldown between item craftings")]
    public float crafting_machine_cooldown = 1f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") // TODO have to check the layers
        {
            if (ready_to_infuse)
            {
                if (gameObject.layer == collider.gameObject.layer)
                {
                    Player player_colliding = collider.GetComponent<Player>();

                    if (player_colliding.current_element_energy >= energy_needed_to_infuse)
                    {
                        InfuseItem(player_colliding.bottled_element);
                        player_colliding.ConsumeElementEnergy(energy_needed_to_infuse);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

        }
    }

    public void TimeToCraft(PlayerItemManager.Items material_1, PlayerItemManager.Items material_2)
    {
        int craft_quality = 0;

        switch (material_1)
        {
            case PlayerItemManager.Items.Tier1_Fuchsia:
                {
                    craft_quality += tier_2_quality_points;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Lime:
                {
                    craft_quality += tier_1_quality_points;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Orange:
                {
                    craft_quality += tier_1_quality_points;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Turquoise:
                {
                    craft_quality += tier_2_quality_points;
                    break;
                }
        }

        switch (material_2)
        {
            case PlayerItemManager.Items.Tier1_Fuchsia:
                {
                    craft_quality += tier_2_quality_points;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Lime:
                {
                    craft_quality += tier_1_quality_points;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Orange:
                {
                    craft_quality += tier_1_quality_points;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Turquoise:
                {
                    craft_quality += tier_2_quality_points;
                    break;
                }
        }

        crafting_machine_points += craft_quality;
        total_crafting_points += craft_quality;

        game_manager.GetComponent<VictoryPointsManager>().AddVictoryPoints(craft_quality, gameObject.layer);

        items_crafted++;
        machine_quality = (total_crafting_points / items_crafted);

        if (crafting_machine_points >= points_to_level_up)
        {
            crafting_machine_level++;

            crafting_machine_points = (crafting_machine_points - points_to_level_up);

            points_to_level_up += level_up_difficulty;            
        }        

        if(crafting_machine_level >= 1)
        {
            crafting_machine_cooldown += 4f; //TODO review the cooldown between crafts to being infused
            ready_to_infuse = true;
        }

        StartCoroutine(MachineCooldown());
    }

    public void InfuseItem(Player.PlayerElementBottled element_to_infuse)
    {
        switch(element_to_infuse)
        {
            case Player.PlayerElementBottled.Fire:
                {
                    break;
                }
            case Player.PlayerElementBottled.Earth:
                {
                    break;
                }
            case Player.PlayerElementBottled.Water:
                {
                    break;
                }
            case Player.PlayerElementBottled.Ice:
                {
                    break;
                }
            case Player.PlayerElementBottled.Plant:
                {
                    break;
                }
            case Player.PlayerElementBottled.Air:
                {
                    break;
                }
            case Player.PlayerElementBottled.Electric:
                {
                    break;
                }
        }
    }

    IEnumerator MachineCooldown()
    {
        yield return new WaitForSeconds(crafting_machine_cooldown);

        ready_to_infuse = false;
        GetComponent<CraftingMachineProcessor_1>().enabled = true;

        StopCoroutine(MachineCooldown());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
