using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlayer : MonoBehaviour
{
    float player_movement_speed = 4.5f;
    float player_item_carrying_speed = 3.5f;

    public float GetWaterBaseSpeed()
    {
        player_movement_speed = 4.5f;
        return player_movement_speed;
    }
    public float GetWaterItemCarryingSpeed()
    {
        player_item_carrying_speed = 3.5f;
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
