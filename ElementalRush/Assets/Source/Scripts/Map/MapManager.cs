using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //------Public variables to fill in the editor------
    //Prefab to place as a ____
    public Transform map_obstacle_prefab;
    public Transform map_boundary_prefab;
    public Transform players_floor_prefab;

    //Size of the map in tiles
    public Vector2Int map_size;

    //Player base from the map
    PlayersMatchBase match_base = null;

    public List<GameObject> blue_spawn_points;
    public List<GameObject> red_spawn_points;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Function to generate the tilemap
    public void GenerateMap()
    {
        string holder_name = "Generated Map";
        if (transform.Find(holder_name))
        {
            DestroyImmediate(transform.Find(holder_name).gameObject);
            transform.Find(holder_name);
        }

        Transform map_holder = new GameObject(holder_name).transform;
        map_holder.parent = transform;

        //Bottom map boundary
        Vector3 bottom_boundary_position = new Vector3(0, 0, 0);
        if (map_size.x % 2 == 0 && map_size.y % 2 == 0)
        {
            bottom_boundary_position.Set(map_size.x / 2, 0.75f, -0.05f);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 != 0)
        {
            bottom_boundary_position.Set((map_size.x / 2 + 0.5f), 0.75f, -0.05f);
        }
        else if (map_size.x % 2 == 0 && map_size.y % 2 != 0)
        {
            bottom_boundary_position.Set(map_size.x / 2, 0.75f, -0.05f);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 == 0)
        {
            bottom_boundary_position.Set((map_size.x / 2 + 0.5f), 0.75f, -0.05f);
        }
        Transform bottom_boundary = Instantiate(map_boundary_prefab, bottom_boundary_position, Quaternion.identity) as Transform;
        bottom_boundary.localScale = new Vector3(map_size.x, 1.5f, 0.1f);
        bottom_boundary.parent = map_holder;
        bottom_boundary.name = "BottomBoundary";

        //Left map boundary
        Vector3 left_boundary_position = new Vector3(0, 0, 0);
        if (map_size.x % 2 == 0 && map_size.y % 2 == 0)
        {
            left_boundary_position.Set(-0.05f, 0.75f, map_size.y / 2);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 != 0)
        {
            left_boundary_position.Set(-0.05f, 0.75f, (map_size.y / 2) + 0.5f);
        }
        else if (map_size.x % 2 == 0 && map_size.y % 2 != 0)
        {
            left_boundary_position.Set(-0.05f, 0.75f, (map_size.y / 2) + 0.5f);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 == 0)
        {
            left_boundary_position.Set(-0.05f, 0.75f, map_size.y / 2);
        }
        Transform left_boundary = Instantiate(map_boundary_prefab, left_boundary_position, Quaternion.identity) as Transform;
        left_boundary.localScale = new Vector3(0.1f, 1.5f, map_size.y);
        left_boundary.parent = map_holder;
        left_boundary.name = "LeftBoundary";

        //Top map boundary
        Vector3 top_boundary_position = new Vector3(0, 0, 0);
        if (map_size.x % 2 == 0 && map_size.y % 2 == 0)
        {
            top_boundary_position.Set(map_size.x / 2, 0.75f, 0.05f + map_size.y);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 != 0)
        {
            top_boundary_position.Set((map_size.x / 2 + 0.5f), 0.75f, 0.05f + map_size.y);
        }
        else if (map_size.x % 2 == 0 && map_size.y % 2 != 0)
        {
            top_boundary_position.Set(map_size.x / 2, 0.75f, 0.05f + map_size.y);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 == 0)
        {
            top_boundary_position.Set((map_size.x / 2 + 0.5f), 0.75f, 0.05f + map_size.y);
        }
        Transform top_boundary = Instantiate(map_boundary_prefab, top_boundary_position, Quaternion.identity) as Transform;
        top_boundary.localScale = new Vector3(map_size.x, 1.5f, 0.1f);
        top_boundary.parent = map_holder;
        top_boundary.name = "TopBoundary";

        //Right map boundary
        Vector3 right_boundary_position = new Vector3(0, 0, 0);
        if (map_size.x % 2 == 0 && map_size.y % 2 == 0)
        {
            right_boundary_position.Set(map_size.x + 0.05f, 0.75f, map_size.y / 2);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 != 0)
        {
            right_boundary_position.Set(map_size.x + 0.05f, 0.75f, (map_size.y / 2) + 0.5f);
        }
        else if (map_size.x % 2 == 0 && map_size.y % 2 != 0)
        {
            right_boundary_position.Set(map_size.x + 0.05f, 0.75f, (map_size.y / 2) + 0.5f);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 == 0)
        {
            right_boundary_position.Set(map_size.x + 0.05f, 0.75f, map_size.y / 2);
        }
        Transform right_boundary = Instantiate(map_boundary_prefab, right_boundary_position, Quaternion.identity) as Transform;
        right_boundary.localScale = new Vector3(0.1f, 1.5f, map_size.y);
        right_boundary.parent = map_holder;
        right_boundary.name = "RightBoundary";

        //Players floor from the battleground
        Transform players_floor = Instantiate(players_floor_prefab) as Transform;
        players_floor.parent = map_holder;
        players_floor.name = "PlayersFloor";
        players_floor.localScale = new Vector3(map_size.x / 10f, 1, map_size.y / 10f);
        players_floor.transform.position = new Vector3(map_size.x / 2f, 0, map_size.y / 2f);

        //Setting the player base trigger
        match_base = GetComponentInChildren<PlayersMatchBase>();
        match_base.SetTriggerBase();

        int spawn_positions = (map_size.x / 2);
        blue_spawn_points[0].transform.position = new Vector3(spawn_positions - 10f, 1f, 0.5f);
        blue_spawn_points[1].transform.position = new Vector3(spawn_positions, 1f, 0.5f);
        blue_spawn_points[2].transform.position = new Vector3(spawn_positions + 10f, 1f, 0.5f);

        red_spawn_points[0].transform.position = new Vector3(spawn_positions - 10f, 1f, map_size.y - 0.5f);
        red_spawn_points[1].transform.position = new Vector3(spawn_positions, 1f, map_size.y - 0.5f);
        red_spawn_points[2].transform.position = new Vector3(spawn_positions + 10f, 1f, map_size.y - 0.5f);
    }
}
