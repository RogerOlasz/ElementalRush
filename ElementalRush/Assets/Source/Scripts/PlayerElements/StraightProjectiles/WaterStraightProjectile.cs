﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterStraightProjectile : MonoBehaviourPun, IPunObservable
{
    [Header("Projectile Attributes")]
    public float projectile_speed;
    public float projectile_range;

    [Header("Element Path GameObject")]
    public string element_path; //TODO it could be a good idea to modify things without code    
    [HideInInspector] public GameObject my_path = null;

    private bool path_instantiated;
    //public Vector3 path_scale;
    //public Vector3 path_position;
    //public Vector3 first_path_pos;
    private Vector3 original_pos;
    private Vector3 projectile_direction;
    public Vector3 projectile_direction_normalized;

    public float distance;

    public void SetProjectileProperties(float _projectile_speed, float _projectile_range)
    {
        projectile_speed = _projectile_speed;
        projectile_range = _projectile_range;
    }

    // Start is called before the first frame update
    void Start()
    {
        original_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        path_instantiated = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(original_pos, transform.position);
        projectile_direction = transform.position - original_pos;
        projectile_direction_normalized = projectile_direction.normalized;

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
            if (distance >= 0.55f && path_instantiated == false)
            {
                my_path = PhotonNetwork.Instantiate("WaterStraightPath", new Vector3(transform.position.x, 0.005f, transform.position.z), transform.rotation);
                path_instantiated = true;
            }
        }

        //if (photonView.IsMine)
        //{
        //    if (distance >= 0.55f && path_instantiated == false)
        //    {
        //        my_path = PhotonNetwork.Instantiate("WaterStraightPath", new Vector3(transform.position.x, 0.005f, transform.position.z), transform.rotation);
        //        my_path.GetPhotonView().Owner.TagObject = this.gameObject;

        //        path_scale = my_path.transform.localScale;
        //        path_position = my_path.transform.position;
        //        first_path_pos = path_position;
        //        path_instantiated = true;
        //    }
        //    else if (my_path != null)
        //    {
        //        projectile_direction = transform.position - original_pos;
        //        projectile_direction_normalized = projectile_direction.normalized;

        //        path_scale.x = distance;
        //        my_path.transform.localScale = path_scale;

        //        path_position.x = (distance / 2 * projectile_direction_normalized.x) + first_path_pos.x;
        //        path_position.z = (distance / 2 * projectile_direction_normalized.z) + first_path_pos.z;

        //        my_path.transform.position = path_position;
        //    }
        //}
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
        if (stream.IsWriting && photonView.IsMine)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
