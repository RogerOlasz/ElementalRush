using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlayer : MonoBehaviour
{
    float player_movement_speed = 5.5f;
    float player_item_carrying_speed = 4.5f;

    public float GetAirBaseSpeed()
    {
        player_movement_speed = 5.5f;
        return player_movement_speed;
    }

    public float GetAirItemCarryingSpeed()
    {
        player_item_carrying_speed = 4.5f;
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
