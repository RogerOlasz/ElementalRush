using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FirePathBehaviour : MonoBehaviourPun, IPunObservable
{
    CrowdControlManager cc_manager;

    public float effect_duration = 5f;
    public float tick_delay = 1f;
    public int erased_energy_per_tick = 4;
    private float timer = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();

        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if(Mathf.Approximately(tick_delay, timer) || timer > tick_delay)
            {
                cc_manager = collider.GetComponent<CrowdControlManager>();

                cc_manager.EraseElementEnergy(erased_energy_per_tick);

                timer = 0;
            }

            timer += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();

        }
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(effect_duration);

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
