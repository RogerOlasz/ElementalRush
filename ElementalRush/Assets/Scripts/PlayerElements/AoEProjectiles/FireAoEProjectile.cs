using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAoEProjectile : MonoBehaviour
{
    public float projectile_speed = 8f;
    public float projectile_range = 6f;
    public float area_radius = 3.5f;

    private float max_height = 4f;
    private bool going_down = false;
    private Vector3 parabolic;
    private Vector3 original_pos;

    // Start is called before the first frame update
    void Start()
    {
        original_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        parabolic = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        parabolic.x = transform.position.x;
        parabolic.z = transform.position.z;

        if (Vector3.Distance(original_pos, parabolic) <= projectile_range && transform.position.y < max_height && going_down == false)
        {
            transform.position += transform.right * (projectile_speed * Time.deltaTime);
            transform.position += transform.up * (projectile_speed * Time.deltaTime);
        }
        else if(Vector3.Distance(original_pos, parabolic) <= projectile_range)
        {
            going_down = true;
            Debug.Log("Projectile height: " + transform.position.y);
            Debug.Log("Projectile lenght: " + Vector3.Distance(original_pos, parabolic));
            transform.position += transform.right * (projectile_speed * Time.deltaTime);
            transform.position -= transform.up * (projectile_speed * Time.deltaTime);
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
