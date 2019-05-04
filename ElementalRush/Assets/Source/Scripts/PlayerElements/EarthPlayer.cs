using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPlayer : MonoBehaviour
{
    private Player player_stats;

    [Header("Element attributes")]
    public float player_movement_speed = 3.5f;
    public float player_item_carrying_speed = 3.5f;

    [Header("Straight attack properties")]
    public int straight_attack_consumption = 20;
    public float straight_projectile_speed = 9f;
    public float straight_projectile_range = 14f;
    public GameObject straight_projectile_effect;
    private EarthStraightProjectile straight_projectile;

    [Header("AoE attack properties")]
    public int aoe_attack_consumption = 15;
    public float aoe_projectile_speed;
    public float aoe_projectile_range;
    public GameObject aoe_projectile_effect;
    //private EarthAoEProjectile aoe_projectile;

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

        straight_projectile = straight_projectile_effect.GetComponent<EarthStraightProjectile>();
        straight_projectile.SetProjectileProperties(straight_projectile_speed, straight_projectile_range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
