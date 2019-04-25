using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAoEProjectile : MonoBehaviour
{
    public float projectile_speed = 8f;
    public float projectile_range = 6f;
    public float area_radius = 3.5f;

    public int projectile_average = 8;
    public float velocity_rate;
    public float visual_height = 4f; //it is ruled by a factor of 1/2

    private Vector3 original_pos;

    private float max_height;
    private float gravity = -10f;
    private Vector3 target_pos;
    private Vector3 relative_target;
    private Rigidbody my_rigidbody;
    private PlayerController p_controller;
    

    Vector3 CalculateLaunchVelocity()
    {
        Vector3 displacement = new Vector3(target_pos.x - transform.position.x, 0, target_pos.z - transform.position.z);
        max_height = Mathf.Pow(relative_target.magnitude, 2) * Mathf.Abs(gravity) / 8 / Mathf.Pow(projectile_speed, 2) * visual_height;
        Vector3 velocity_y = Vector3.up * Mathf.Sqrt(-2 * gravity * max_height * Mathf.Pow(p_controller.last_direction_r2_no_normal.magnitude, 2));
        Vector3 velocity_x_z = displacement / (Mathf.Sqrt(-2 * max_height / gravity) + Mathf.Sqrt(2 * (-max_height) / gravity));

        return (velocity_x_z + velocity_y);
    }

    //Start is called before the first frame update
    void Start()
    {
        p_controller = FindObjectOfType<PlayerController>();
        relative_target = new Vector3(p_controller.last_direction_r2_no_normal.x * projectile_range, 0, p_controller.last_direction_r2_no_normal.y * projectile_range);
        original_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        target_pos = original_pos + relative_target;
        velocity_rate = projectile_speed / projectile_average;

        Physics.gravity = Vector3.up * gravity;
        my_rigidbody = transform.GetComponent<Rigidbody>();
        my_rigidbody.useGravity = true;

        my_rigidbody.velocity = CalculateLaunchVelocity();
    }

    // Update is called once per frame
    void Update()
    {

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
