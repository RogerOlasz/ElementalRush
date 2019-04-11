using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour
{
    UIManager ui_manager = null;

    public Vector2Int tile_coord;
    private bool player_is_on;

    private float duration = 0;

    private Color brown;
    private Color normal_color;

    public enum TileType 
    {
        Normal = 0,
        Base,
        Fire,
        Earth,
        Water,
        Ice,
        Plant,
        Air,
        Electric
    };

    public TileType type;

    public void SetTile(TileType tile_type, Vector2Int _tile_coord)
    {
        tile_coord = _tile_coord;
        type = tile_type;
    }

    public void SetDuration(float new_duration)
    {
        duration = new_duration;
    }

    public void SetTileCoord(Vector2Int new_tile_coord)
    {
        tile_coord = new_tile_coord;
    }

    public Vector2Int GetTileCoord()
    {
        return tile_coord;
    }

    public Tile GetTile()
    {
        return this;
    }

    public void ModifyTileType(TileType new_type)
    {
        type = new_type;
    }

    //UNDER CONSTRUCTION
    //private void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.tag == "Player")
    //    {
    //        Collider[] colliders = new Collider[4];
    //        colliders = Physics.OverlapBox(collider.transform.position, new Vector3(collider.transform.position.x / 2, collider.transform.position.y / 2, collider.transform.position.z / 2));

    //        Collider nearest_collider = null;
    //        float min_sqr_distance = Mathf.Infinity;

    //        for (int i = 0; i < colliders.Length; i++)
    //        {
    //            float sqr_distance_to_center = (collider.transform.position - colliders[i].transform.position).sqrMagnitude;

    //            if (sqr_distance_to_center < min_sqr_distance && colliders[i].gameObject.tag != "Player" && gameObject.tag == "Tilemap")
    //            {
    //                min_sqr_distance = sqr_distance_to_center;
    //                nearest_collider = colliders[i];
    //            }
    //        }

    //        Tile tile_script;
    //        tile_script = nearest_collider.GetComponent<Tile>();

    //        tile_script.player_is_on = true;
    //        tile_script.type = TileType.Fire;
    //        Debug.Log("Player is on: " + tile_script.transform);
    //        //if(nearest_collider.transform.name == transform.name)
    //        //{
    //        //    player_is_on = true;
    //        //    type = TileType.Fire;
    //        //    Debug.Log("Player is on: " + transform);
    //        //}  
    //    }
    //}

    //TODO Player can only be on one tile
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player_is_on = true;
            //Debug.Log("Player is on: " + transform);
            if(this.type == TileType.Base && ui_manager != null)
            {
                ui_manager.OpenElementChangingMenu(); //TODO Multiple tile collision are not able to give a good experience over element changer menu
            }
            else if (ui_manager != null)
            {
                ui_manager.CloseElementChangingMenu();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player_is_on = false;

            //if (this.type != TileType.Base)
            //{
            //    ui_manager.CloseElementChangingMenu();
            //}
        }
    }

    void Start()
    {
        player_is_on = false;
        normal_color = new Vector4(0.4f, 0.7f, 0.4f, 1);
        brown = new Vector4(0.804f, 0.521f, 0.247f, 1);

        ui_manager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        //Debug.Log("Updating Tiles...");
        switch (type)
        {
            case TileType.Normal:
                {
                    GetComponent<Renderer>().material.color = normal_color;
                    break;
                }
            case TileType.Base:
                {
                    GetComponent<Renderer>().material.color = Color.grey;
                    break;
                }
            case TileType.Fire:
                {
                    GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case TileType.Earth:
                {
                    GetComponent<Renderer>().material.color = brown; 
                    break;
                }
            case TileType.Water:
                {
                    GetComponent<Renderer>().material.color = Color.blue;
                    break;
                }
            case TileType.Ice:
                {
                    GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                }
            case TileType.Plant:
                {
                    GetComponent<Renderer>().material.color = Color.green;
                    break;
                }
            case TileType.Air:
                {
                    GetComponent<Renderer>().material.color = Color.magenta;
                    break;
                }
            case TileType.Electric:
                {
                    GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                }
            default:
                {
                    type = TileType.Normal;
                    break;
                }
        }
    }
}
