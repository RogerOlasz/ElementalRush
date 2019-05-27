using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public GameObject player;
    public float speed_factor;

    private bool touch_start = false;
    private Vector2 point_a;
    private Vector2 point_b;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            point_a = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        if(Input.GetMouseButton(0))
        {
            touch_start = true;
            point_b = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touch_start = false;
        }
    }

    void FixedUpdate()
    {
        if(touch_start)
        {
            Vector2 offset = point_b - point_a;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            MoveCharacter(direction * -1);
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        player.transform.Translate(direction * speed_factor * Time.deltaTime);
    }
}
