using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricStraightProjectile : MonoBehaviour
{
    [Header("Projectile Attributes")]
    public float projectile_speed;
    public float projectile_range;

    private Vector3 original_pos;

    public void SetProjectileProperties(float _projectile_speed, float _projectile_range)
    {
        projectile_speed = _projectile_speed;
        projectile_range = _projectile_range;
    }

    // Start is called before the first frame update
    void Start()
    {
        original_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(original_pos, transform.position) <= projectile_range)
        {
            transform.position += transform.right * (projectile_speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MapBoundary")
        {
            Destroy(gameObject);
        }
    }
}
