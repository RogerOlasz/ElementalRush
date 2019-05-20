using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EarthStraightProjectile : MonoBehaviourPun, IPunObservable
{
    [Header("Projectile Attributes")]
    public float projectile_speed;
    public float projectile_range;

    [Header("Element Path GameObject")]
    public string element_path; //TODO it could be a good idea to modify things without code
    private int tile_counter = 1;

    private Vector3 original_pos;
    private Vector3 first_path_go_pos;
    private Vector3 projectile_direction;
    private Vector3 projectile_direction_normalized;

    private float distance;

    public void SetProjectileProperties(float _projectile_speed, float _projectile_range)
    {
        projectile_speed = _projectile_speed;
        projectile_range = _projectile_range;
    }

    // Start is called before the first frame update
    void Start()
    {
        original_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(original_pos, transform.position);

        if (distance <= projectile_range)
        {
            transform.position += transform.right * (projectile_speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

        if (photonView.IsMine)
        {
            if (distance >= tile_counter)
            {
                projectile_direction = transform.position - original_pos;
                projectile_direction_normalized = projectile_direction.normalized;

                if (tile_counter == 1)
                {
                    GameObject tmp_path;
                    tmp_path = PhotonNetwork.Instantiate("EarthStraightPath", new Vector3(transform.position.x, 0.6f, transform.position.z), transform.rotation);
                    first_path_go_pos = transform.position;

                }
                else if (tile_counter > 1)
                {
                    GameObject tmp_path;
                    Vector3 tmp_path_position;
                    tmp_path_position = new Vector3((first_path_go_pos.x + ((tile_counter - 1) * projectile_direction_normalized.x)), 0.6f, (first_path_go_pos.z + ((tile_counter - 1) * projectile_direction_normalized.z)));
                    tmp_path = PhotonNetwork.Instantiate("EarthStraightPath", tmp_path_position, transform.rotation);
                }

                tile_counter++;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MapBoundary")
        {
            Destroy(gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
