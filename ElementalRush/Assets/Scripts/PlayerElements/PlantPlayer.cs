using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlayer : MonoBehaviour
{
    Player player_stats;

    public float player_movement_speed = 5;
    public float player_item_carrying_speed = 4;

    public float GetPlantBaseSpeed()
    {
        return player_movement_speed;
    }

    public float GetPlantItemCarryingSpeed()
    {
        return player_item_carrying_speed;
    }

    public void StraightAttack()
    {

    }

    public void AoEAttack()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player_stats = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
