using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlayer : MonoBehaviour
{
    Player player_stats;

    public float player_movement_speed = 5;
    public float player_item_carrying_speed = 4;

    public void SetPlantBaseSpeed()
    {
        player_stats.movement_speed = player_movement_speed;
    }

    public void SetPlantItemCarryingSpeed()
    {
        player_stats.item_carrying_speed = player_item_carrying_speed;
    }

    public void StraightAttack()
    {

    }

    public void AoEAttack()
    {

    }

    // Start is called before the first frame update
    void Awake()
    {
        player_stats = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
