using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStraightProjectile : MonoBehaviour
{
    public float projectile_speed = 16f;
    public float projectile_range = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile_speed != 0)
        {
            transform.position += transform.right * (projectile_speed * Time.deltaTime);
        }
    }
}
