using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterPathBehaviour : MonoBehaviourPun, IPunObservable
{
    //private Vector3 real_position;
    //private Quaternion real_rotation;
    //private Vector3 real_scale;

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

            list_of_cc.Add(cc_manager); 
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
        StartCoroutine(AttackDuration());
    }

    // Update is called once per frame
    void Update()
    {        
        //if (photonView.IsMine)
        //{

        //}
        //else
        //{            
        //    //transform.position = Vector3.Lerp(transform.position, real_position, Time.deltaTime * 15);
        //    //transform.rotation = Quaternion.Lerp(transform.rotation, real_rotation, Time.deltaTime * 30);
        //    //transform.localScale = Vector3.Lerp(transform.localScale, real_scale, Time.deltaTime * 15);
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
