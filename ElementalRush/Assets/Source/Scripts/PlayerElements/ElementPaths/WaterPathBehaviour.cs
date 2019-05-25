using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterPathBehaviour : MonoBehaviourPun, IPunInstantiateMagicCallback,IPunObservable
{
    private Vector3 real_position;
    private Quaternion real_rotation;
    private Vector3 real_scale;

    WaterStraightProjectile projectile_script = null;

    private bool initial_state;
    private Vector3 path_scale;
    private Vector3 path_position;
    private Vector3 first_path_pos;

    CrowdControlManager cc_manager;
    List<CrowdControlManager> list_of_cc;

    public float effect_duration = 10f;
    public float slow_percentage = 0.2f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {            
            cc_manager = collider.GetComponent<CrowdControlManager>();
            cc_manager.ApplySlowCC(slow_percentage);

            list_of_cc.Add(cc_manager); //TODO search If I already have a reference to that cc_manager
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {           
            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();
            cc_manager.RemoveSlowCC(slow_percentage);

            list_of_cc.Remove(cc_manager);
        }
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(effect_duration);
        
        if (list_of_cc.Count > 0)
        {
            for (int i = 0; i < list_of_cc.Count; i++)
            {
                list_of_cc[i].RemoveSlowCC(slow_percentage);
            }
        }

        Destroy(gameObject);
    }

    void Awake()
    {
        list_of_cc = new List<CrowdControlManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject projectile = PhotonView.Find(photonView.ViewID - 1).transform.gameObject; //TODO: not so stable solution xD
        projectile_script = projectile.GetComponent<WaterStraightProjectile>();

        initial_state = false;

        StartCoroutine(AttackDuration());
    }

    // Update is called once per frame
    void Update()
    {
        if (initial_state == false)
        {
            path_scale = transform.localScale;
            path_position = transform.position;
            first_path_pos = path_position;
            initial_state = true;
        }
        else if (projectile_script != null)
        {
            path_scale.x = projectile_script.distance;
            transform.localScale = path_scale;

            path_position.x = (projectile_script.distance / 2 * projectile_script.projectile_direction_normalized.x) + first_path_pos.x;
            path_position.z = (projectile_script.distance / 2 * projectile_script.projectile_direction_normalized.z) + first_path_pos.z;

            transform.position = path_position;
        }

        //if (photonView.IsMine)
        //{

        //}
        //else
        //{
        //    if (projectile_script.distance >= 0.55f && projectile_script.path_instantiated == false)
        //    {
        //        projectile_script.path_scale = transform.localScale;
        //        projectile_script.path_position = transform.position;
        //        projectile_script.first_path_pos = projectile_script.path_position;
        //        projectile_script.path_instantiated = true;
        //    }
        //    else if (projectile_script.my_path != null)
        //    {
        //        projectile_script.projectile_direction = projectile_script.transform.position - projectile_script.original_pos;
        //        projectile_script.projectile_direction_normalized = projectile_script.projectile_direction.normalized;

        //        projectile_script.path_scale.x = projectile_script.distance;
        //        transform.localScale = projectile_script.path_scale;

        //        projectile_script.path_position.x = (projectile_script.distance / 2 * projectile_script.projectile_direction_normalized.x) + projectile_script.first_path_pos.x;
        //        projectile_script.path_position.z = (projectile_script.distance / 2 * projectile_script.projectile_direction_normalized.z) + projectile_script.first_path_pos.z;

        //        transform.position = projectile_script.path_position;
        //    }

        //    //transform.position = Vector3.Lerp(transform.position, real_position, Time.deltaTime * 15);
        //    //transform.rotation = Quaternion.Lerp(transform.rotation, real_rotation, Time.deltaTime * 30);
        //    //transform.localScale = Vector3.Lerp(transform.localScale, real_scale, Time.deltaTime * 15);
        //}
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //if (!photonView.IsMine)
        //{
        //    GameObject projectile = (GameObject)info.Sender.TagObject;
        //    transform.position = projectile.transform.position;
        //    transform.rotation = projectile.transform.rotation;
        //}
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(transform.position);
            //stream.SendNext(transform.rotation);
            //stream.SendNext(transform.localScale);
        }
        else if (stream.IsReading)
        {
            //real_position = (Vector3)stream.ReceiveNext();
            //real_rotation = (Quaternion)stream.ReceiveNext();
            //real_scale = (Vector3)stream.ReceiveNext();
        }
    }
}
