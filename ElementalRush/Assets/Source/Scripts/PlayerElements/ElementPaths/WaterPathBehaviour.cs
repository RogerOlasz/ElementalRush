using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterPathBehaviour : MonoBehaviourPun, IPunObservable
{
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

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (list_of_cc.Count > 0)
        {
            for (int i = 0; i < list_of_cc.Count; i++)
            {
                list_of_cc[i].RemoveSlowCC(slow_percentage);
            }
        }
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
