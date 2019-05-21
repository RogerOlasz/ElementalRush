using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterPathBehaviour : MonoBehaviourPun, IPunObservable
{
    CrowdControlManager cc_manager;

    public float effect_duration = 10f;
    public float slow_percentage = 0.2f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();
            cc_manager.ApplySlowCC(slow_percentage);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();
            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();
            cc_manager.RemoveSlowCC(slow_percentage);
        }
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(effect_duration);
        cc_manager.RemoveSlowCC(slow_percentage);
        Destroy(gameObject);
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
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
