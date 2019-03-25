using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //------Public variables to fill in the editor------
    //Prefab to place as a tile
    public Transform tile_prefab;
    public Transform map_obstacle_prefab;
    public Transform map_boundary_prefab;

    public Transform players_floor;

    //Size of the map in tiles
    public Vector2Int map_size;

    //Outline between tiles
    [Range(0,1)]
    public float outline_percent;

    [System.Serializable]
    public struct Tile
    {
        //Maybe would be better have a Vector2Int
        public int x, y;
        public Transform tile_GO;

        public Tile(int _x, int _y, Transform _tile_GO)
        {
            x = _x;
            y = _y;
            tile_GO = _tile_GO;
        }

        public void SetRealPos(Vector3 new_real_pos)
        {
            tile_GO.transform.Translate(new_real_pos);
        }

        public Vector2Int GetTile()
        {
            Vector2Int tile_pos = new Vector2Int(x, y);

            return tile_pos;
        }
    }

    public List<Tile> tiles_list;

    public Vector2Int GetTilePosFromPos(Vector3 space_position)
    {
        Vector2Int to_return = new Vector2Int(-1, -1);

        Bounds aabb;

        for (int i = 0; i < tiles_list.Count; i++)
        {
            Vector3 tmp;
            tmp = tiles_list[i].tile_GO.transform.position;

            aabb = new Bounds();
            aabb.Encapsulate(new Vector3(tmp.x - 0.5f, -1, tmp.z - 0.5f));
            aabb.Encapsulate(new Vector3(tmp.x + 0.5f, 1, tmp.z + 0.5f));

            if (aabb.Contains(space_position))
            {
                to_return.Set(tiles_list[i].x, tiles_list[i].y);

                return to_return;
            }
        }

        return to_return;
    }

    //Function to generate the tilemap
    public void GenerateMap()
    {
        tiles_list = new List<Tile>();

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
                //Vector3 tile_position = new Vector3(-map_size.x / 2 + 0.5f + x, 0, -map_size.y / 2 + 0.5f + y);
                Vector3 tile_position = new Vector3(x + 0.5f, 0, y + 0.5f);

                Transform new_tile = Instantiate(tile_prefab, tile_position, Quaternion.Euler(Vector3.right * 90)) as Transform;

                new_tile.localScale = Vector3.one * (1 - outline_percent);
                new_tile.name = "Tile_x" + x + "_y" + y;
                new_tile.parent = map_holder;

                Tile tmp_tile = new Tile(x, y, new_tile);
                //Debug.Log(tmp_tile.GetTile());
                tiles_list.Add(tmp_tile);

                //Vector2 tmp = new Vector2(tile_position.x, tile_position.z);
                //tiles_list.Add(tmp);
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

        Vector3 obstacle_position = new Vector3(2 + 0.5f, 0.5f, 8 + 0.5f);

        Transform new_obstacle = Instantiate(map_obstacle_prefab, obstacle_position, Quaternion.identity) as Transform;

        new_obstacle.localScale = Vector3.one * (1 - outline_percent);
        new_obstacle.parent = map_holder;

        players_floor.localScale = new Vector3(map_size.x / 10f, 1, map_size.y / 10f);
        players_floor.transform.position = new Vector3(map_size.x / 2 + 0.5f, 0, map_size.y / 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
