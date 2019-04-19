using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    Player player_stats;

    public float player_movement_speed = 5;
    public float player_item_carrying_speed = 4;

    //Straight attack projectile attributes
    public Transform straight_projectile_prefab;
    public List<GameObject> projectile_vfx_list = new List<GameObject>();
    private GameObject effect_to_spawn;
    
    public void SetFireBaseSpeed()
    {
        player_stats.movement_speed = player_movement_speed;
    }

    public void SetFireItemCarryingSpeed()
    {
        player_stats.item_carrying_speed = player_item_carrying_speed;
    }

    public void StraightAttack(Vector3 projectile_direction)
    {
        GameObject vfx;

        if(player_stats != null)
        {
            vfx = Instantiate(effect_to_spawn, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Player stats is null.");
        }

       //player_stats.element_energy -= 7; //TODO: Fill all the attacks with their behaveour and the respectives energy costs.
    }

    public void AoEAttack()
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {        
        player_stats = GetComponent<Player>();
        effect_to_spawn = projectile_vfx_list[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
