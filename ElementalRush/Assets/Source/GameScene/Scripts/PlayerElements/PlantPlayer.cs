using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlantPlayer : MonoBehaviourPun
{
    private Player player_stats;

    [Header("Element attributes")]
    public float player_movement_speed = 5;
    public float player_item_carrying_speed = 4;

    [Header("Straight attack properties")]
    public int straight_attack_consumption = 7;
    public float straight_projectile_speed = 8f;
    public float straight_projectile_range = 5f;
    public GameObject straight_projectile_effect;
    private PlantStraightProjectile straight_projectile;

    [Header("AoE attack properties")]
    public int aoe_attack_consumption = 10;
    public float aoe_projectile_speed;
    public float aoe_projectile_range;
    public GameObject aoe_projectile_effect;
    //private PlantAoEProjectile aoe_projectile;

    public void SetPlantBaseSpeed()
    {
        player_stats.movement_speed = player_movement_speed;
    }

    public void SetPlantItemCarryingSpeed()
    {
        player_stats.item_carrying_speed = player_item_carrying_speed;
    }

    public void SetPlantStraightConsumption()
    {
        player_stats.straight_attack_player_consumption = straight_attack_consumption;
    }

    public void SetPlantAoEConsumption()
    {
        player_stats.aoe_attack_player_consumption = aoe_attack_consumption;
    }

    public void StraightAttack()
    {
        GameObject straight_attack_vfx;

        straight_attack_vfx = PhotonNetwork.Instantiate("PlantStraightProjectile", transform.position, transform.rotation);
    }

    public void AoEAttack()
    {

    }

    // Start is called before the first frame update
    void Awake()
    {
        player_stats = GetComponent<Player>();

        straight_projectile = straight_projectile_effect.GetComponent<PlantStraightProjectile>();
        straight_projectile.SetProjectileProperties(straight_projectile_speed, straight_projectile_range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
