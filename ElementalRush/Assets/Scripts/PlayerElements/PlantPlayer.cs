﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlayer : MonoBehaviour
{
    float player_movement_speed = 5;
    float player_item_carrying_speed = 4;

    public float GetPlantBaseSpeed()
    {
        player_movement_speed = 5;
        return player_movement_speed;
    }

    public float GetPlantItemCarryingSpeed()
    {
        player_item_carrying_speed = 4;
        return player_item_carrying_speed;
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
