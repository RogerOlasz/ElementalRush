using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    Player player_stats;

    public float player_movement_speed = 5;
    public float player_item_carrying_speed = 4;

    public int straight_attack_consumption = 7;
    public int aoe_attack_consumption = 10;

    //Straight attack projectile attributes
    private GameObject effect_to_spawn;
    public List<GameObject> projectile_vfx_list = new List<GameObject>();

    public void SetFireBaseSpeed()
    {
        player_stats.movement_speed = player_movement_speed;
    }

    public void SetFireItemCarryingSpeed()
    {
        player_stats.item_carrying_speed = player_item_carrying_speed;
    }

    public void SetFireStraightConsumption()
    {
        player_stats.straight_attack_player_consumption = straight_attack_consumption;
    }

    public void SetFireAoEConsumption()
    {
        player_stats.aoe_attack_player_consumption = aoe_attack_consumption;
    }

    public void StraightAttack()
    {
        GameObject vfx;

        vfx = Instantiate(effect_to_spawn, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        vfx.transform.localRotation = transform.rotation;
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
