using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody player_rigid_body;

    void Start()
    {
        player_rigid_body = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void FixedUpdate()
    {
        player_rigid_body.MovePosition(player_rigid_body.position + velocity * Time.fixedDeltaTime);
    }

    public Vector3 GetPosition()
    {
        return player_rigid_body.position;
    }
}
