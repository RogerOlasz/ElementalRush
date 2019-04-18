using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEPRACATEDMapManager : MonoBehaviour
{
    //------Public variables to fill in the editor------
    //Prefab to place as a tile
    public Transform tile_prefab;
    public Transform map_obstacle_prefab;
    public Transform map_boundary_prefab;

    public Transform players_floor;

    Tile base_tile;

    //Size of the map in tiles
    public Vector2Int map_size;

    //Outline between tiles
    [Range(0,1)]
    public float outline_percent;

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
        //Structure just to place the GO prefabs created into a GO called Generated Map and have all well organized
        string holder_name = "Generated Map";
        if (transform.Find(holder_name))
        {
            DestroyImmediate(transform.Find(holder_name).gameObject);
            transform.Find(holder_name);
        }

        Transform map_holder = new GameObject(holder_name).transform;
        map_holder.parent = transform;

        //Loops to fill the map size with the prefab tiles centering it on the world center 0,0,0
        for (int x = 0; x < map_size.x; x++)
        {
            for (int y = 0; y < map_size.y; y++)
            {
                //Defining the tile position centering the 0,0 of the tilemap
                Vector3 tile_position = new Vector3(x + 0.5f, 0, y + 0.5f);

                Transform new_tile = Instantiate(tile_prefab, tile_position, Quaternion.Euler(Vector3.right * 90)) as Transform;

                new_tile.localScale = Vector3.one * (1 - outline_percent);
                new_tile.name = "Tile_x" + x + "_y" + y;
                new_tile.parent = map_holder;

                if (y == 0)
                {
                    base_tile = new_tile.GetComponent<Tile>();
                    base_tile.ModifyTileType(Tile.TileType.Base);
                }
                else if (y == (map_size.y - 1))
                {
                    base_tile = new_tile.GetComponent<Tile>();
                    base_tile.ModifyTileType(Tile.TileType.Base);
                }
            }
        }

        for (int x = 0; x < map_size.x; x++)
        {
            Vector3 boundary_position = new Vector3(x + 0.5f, 0.75f, -0.05f);
            Transform new_boundary = Instantiate(map_boundary_prefab, boundary_position, Quaternion.identity) as Transform;
            new_boundary.parent = map_holder;
        }

        for (int y = 0; y < map_size.y; y++)
        {
            Vector3 boundary_position = new Vector3(-0.05f, 0.75f, y + 0.5f);
            Transform new_boundary = Instantiate(map_boundary_prefab, boundary_position, Quaternion.Euler(Vector3.up * 90)) as Transform;
            new_boundary.parent = map_holder;
        }

        for (int x = 0; x < map_size.x; x++)
        {
            Vector3 boundary_position = new Vector3(x + 0.5f, 0.75f, (0.05f + map_size.y));
            Transform new_boundary = Instantiate(map_boundary_prefab, boundary_position, Quaternion.identity) as Transform;
            new_boundary.parent = map_holder;
        }

        for (int y = 0; y < map_size.y; y++)
        {
            Vector3 boundary_position = new Vector3((0.05f + map_size.x), 0.75f, y + 0.5f);
            Transform new_boundary = Instantiate(map_boundary_prefab, boundary_position, Quaternion.Euler(Vector3.up * 90)) as Transform;
            new_boundary.parent = map_holder;
        }

        //Vector3 obstacle_position = new Vector3(2 + 0.5f, 0.5f, 8 + 0.5f);

        //Transform new_obstacle = Instantiate(map_obstacle_prefab, obstacle_position, Quaternion.identity) as Transform;

        //new_obstacle.localScale = Vector3.one * (1 - outline_percent);
        //new_obstacle.parent = map_holder;

        players_floor.localScale = new Vector3(map_size.x / 10f, 1, map_size.y / 10f);

        if(map_size.x % 2 == 0 && map_size.y % 2 == 0) 
        {
            players_floor.transform.position = new Vector3(map_size.x / 2, 0, map_size.y / 2);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 != 0)
        {
            players_floor.transform.position = new Vector3(map_size.x / 2 + 0.5f, 0, map_size.y / 2 + 0.5f);
        }
        else if (map_size.x % 2 == 0 && map_size.y % 2 != 0)
        {
            players_floor.transform.position = new Vector3(map_size.x / 2, 0, map_size.y / 2 + 0.5f);
        }
        else if (map_size.x % 2 != 0 && map_size.y % 2 == 0)
        {
            players_floor.transform.position = new Vector3(map_size.x / 2 + 0.5f, 0, map_size.y / 2);
        }

    }
}
