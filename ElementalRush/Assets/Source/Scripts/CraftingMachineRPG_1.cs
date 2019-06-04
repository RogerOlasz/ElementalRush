using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMachineRPG_1 : MonoBehaviour
{
    public int points_to_level_up;

    private int crafting_points = 0;
    private int crafting_level = 0;

    public void CraftFinished(PlayerItemManager.Items material_1, PlayerItemManager.Items material_2)
    {
        int craft_quality = 0;

        switch (material_1)
        {
            case PlayerItemManager.Items.Tier1_Fuchsia:
                {
                    craft_quality += 5;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Lime:
                {
                    craft_quality += 2;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Orange:
                {
                    craft_quality += 2;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Turquoise:
                {
                    craft_quality += 5;
                    break;
                }
        }

        switch (material_2)
        {
            case PlayerItemManager.Items.Tier1_Fuchsia:
                {
                    craft_quality += 5;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Lime:
                {
                    craft_quality += 2;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Orange:
                {
                    craft_quality += 2;
                    break;
                }
            case PlayerItemManager.Items.Tier1_Turquoise:
                {
                    craft_quality += 5;
                    break;
                }
        }

        crafting_points += craft_quality;

        if(crafting_points >= points_to_level_up)
        {
            crafting_level += 1;

            points_to_level_up += 4;
            crafting_points = 0;
        }
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
