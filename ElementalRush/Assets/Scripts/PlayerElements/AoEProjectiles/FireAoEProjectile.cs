using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAoEProjectile : MonoBehaviour
{
    public float projectile_speed = 8f;
    public float projectile_range = 6f;
    public float area_radius = 3.5f;

    private Vector3 original_pos;

    //private float max_height = 4f;
    //private float gravity = -18f;
    //private Vector3 target_pos;
    //private Rigidbody my_rigidbody;

    //Vector3 CalculateLaunchVelocity()
    //{
    //    Vector3 displacement = new Vector3(target_pos.x - transform.position.x, 0, target_pos.z - transform.position.z);

    //    Vector3 velocity_y = Vector3.up * Mathf.Sqrt(-2 * gravity * max_height);
    //    Vector3 velocity_x_z = displacement / (Mathf.Sqrt(-2 * max_height / gravity) + Mathf.Sqrt(2 * (-max_height) / gravity));

    //    return (velocity_x_z + velocity_y);
    //}

    // Start is called before the first frame update
    void Start()
    {
        original_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //target_pos = new Vector3(transform.position.x + projectile_range, transform.position.y, transform.position.z + projectile_range);

        //Physics.gravity = Vector3.up * gravity;
        //my_rigidbody = transform.GetComponent<Rigidbody>();
        //my_rigidbody.useGravity = true;

        //my_rigidbody.velocity = CalculateLaunchVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(original_pos, transform.position) <= projectile_range)
        {
            transform.position += transform.right * (projectile_speed * Time.deltaTime);
            //my_rigidbody.velocity = CalculateLaunchVelocity();
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

        if (collider.gameObject.tag == "MapGround")
        {
            Destroy(gameObject);
        }
    }
}
