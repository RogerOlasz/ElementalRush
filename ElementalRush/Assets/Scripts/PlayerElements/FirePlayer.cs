using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    Player player_stats;

    public float player_movement_speed = 5;
    public float player_item_carrying_speed = 4;

    public float GetFireBaseSpeed()
    {
        return player_movement_speed;
    }

    public float GetFireItemCarryingSpeed()
    {
        return player_item_carrying_speed;
    }

    public void StraightAttack()
    {
        player_stats.element_energy -= 7; //TODO: Fill all the attacks with their behaveour and the respectives energy costs.
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
