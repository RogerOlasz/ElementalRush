using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMachineRPG_1 : MonoBehaviour
{
    [SerializeField] private int crafting_points = 0;
    [SerializeField] private int crafting_level = 0;
    [SerializeField] private float machine_quality = 0;
    [SerializeField] private int items_crafted = 0;
    [SerializeField] private int total_crafting_points = 0;

    [Header("Machine Attributes")]
    public int points_to_level_up;

    public int tier_1_quality_points;
    public int tier_2_quality_points;

    public int level_up_difficulty;

    [Header("Cooldown between item craftings")]
    public float crafting_machine_cooldown = 1f;

    public void CraftFinished(PlayerItemManager.Items material_1, PlayerItemManager.Items material_2)
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

        crafting_points += craft_quality;
        total_crafting_points += craft_quality;

        items_crafted++;
        machine_quality = (total_crafting_points / items_crafted)*10;

        if (crafting_points >= points_to_level_up)
        {
            crafting_level++;

            crafting_points = (crafting_points - points_to_level_up);

            points_to_level_up += level_up_difficulty;            
        }        

        StartCoroutine(MachineCooldown());
    }

    IEnumerator MachineCooldown()
    {
        yield return new WaitForSeconds(crafting_machine_cooldown);

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
