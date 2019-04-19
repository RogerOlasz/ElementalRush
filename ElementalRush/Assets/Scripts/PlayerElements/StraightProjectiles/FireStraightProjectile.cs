using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStraightProjectile : MonoBehaviour
{
    public float projectile_speed = 16f;
    public float projectile_range = 8f;

    private Vector3 original_pos;

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
