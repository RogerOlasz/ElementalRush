using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlayer : MonoBehaviour
{
    float player_movement_speed = 6.5f;
    float player_item_carrying_speed = 5;

    public float GetElectricBaseSpeed()
    {
        player_movement_speed = 6.5f;
        return player_movement_speed;
    }

    public float GetElectricItemCarryingSpeed()
    {
        player_item_carrying_speed = 5;
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
