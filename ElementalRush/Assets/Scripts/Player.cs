using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float move_speed = 5;
    PlayerController controller;
    GameObject map;
    Vector2Int on_tile;

    void Start()
    {
        //controller = GetComponent<PlayerController>();
        map = GameObject.Find("TileMap");
        on_tile = new Vector2Int(0, 0);
    }

    void Update()
    {
        //Vector3 move_input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Vector3 move_velocity = move_input.normalized * move_speed;
        //controller.Move(move_velocity);

        //on_tile = tilemap.GetTilePosFromPos(controller.GetPosition());

        //Debug.Log(on_tile);
    }
}
