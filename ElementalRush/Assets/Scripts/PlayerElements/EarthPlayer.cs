using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPlayer : MonoBehaviour
{
    Player player_stats;

    public float player_movement_speed = 3.5f;
    public float player_item_carrying_speed = 3.5f;

    public int straight_attack_consumption = 20;
    public int aoe_attack_consumption = 15;

    //Straight attack projectile attributes
    public GameObject straight_projectile_effect;
    public GameObject aoe_projectile_effect;

    public void SetEarthBaseSpeed()
    {
        player_stats.movement_speed = player_movement_speed;
    }

    public void SetEarthItemCarryingSpeed()
    {
        player_stats.item_carrying_speed = player_item_carrying_speed;
    }

    public void SetEarthStraightConsumption()
    {
        player_stats.straight_attack_player_consumption = straight_attack_consumption;
    }

    public void SetEarthAoEConsumption()
    {
        player_stats.aoe_attack_player_consumption = aoe_attack_consumption;
    }

    public void StraightAttack()
    {
        GameObject straight_attack_vfx;

        straight_attack_vfx = Instantiate(straight_projectile_effect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        straight_attack_vfx.transform.localRotation = transform.rotation;
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
