using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ElectricPlayer : MonoBehaviourPun
{
    private Player player_stats;

    [Header("Element attributes")]
    public float player_movement_speed = 6.5f;
    public float player_item_carrying_speed = 5f;

    [Header("Straight attack properties")]
    public int straight_attack_consumption = 2;
    public float straight_projectile_speed = 60f;
    public float straight_projectile_range = 5f;
    public GameObject straight_projectile_effect;
    private ElectricStraightProjectile straight_projectile;

    [Header("AoE attack properties")]
    public int aoe_attack_consumption = 10;
    public float aoe_projectile_speed;
    public float aoe_projectile_range;
    public GameObject aoe_projectile_effect;
    //private ElectricAoEProjectile aoe_projectile;

    public void SetElectricBaseSpeed()
    {
        player_stats.movement_speed = player_movement_speed;
    }

    public void SetElectricItemCarryingSpeed()
    {
        player_stats.item_carrying_speed = player_item_carrying_speed;
    }

    public void SetElectricStraightConsumption()
    {
        player_stats.straight_attack_player_consumption = straight_attack_consumption;
    }

    public void SetElectricAoEConsumption()
    {
        player_stats.aoe_attack_player_consumption = aoe_attack_consumption;
    }

    public void StraightAttack()
    {
        GameObject straight_attack_vfx;

        straight_attack_vfx = PhotonNetwork.Instantiate("ElectricStraightProjectile", new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        straight_attack_vfx.transform.localRotation = transform.rotation;
    }

    public void AoEAttack()
    {
        //GameObject aoe_attack_vfx;

        //aoe_attack_vfx = Instantiate(aoe_projectile_effect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //aoe_attack_vfx.transform.localRotation = transform.rotation;
    }

    // Start is called before the first frame update
    void Awake()
    {
        player_stats = GetComponent<Player>();

        straight_projectile = straight_projectile_effect.GetComponent<ElectricStraightProjectile>();
        straight_projectile.SetProjectileProperties(straight_projectile_speed, straight_projectile_range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
