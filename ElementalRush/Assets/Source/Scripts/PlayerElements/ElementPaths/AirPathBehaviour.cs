using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AirPathBehaviour : MonoBehaviourPun, IPunObservable
{
    CrowdControlManager cc_manager;
    List<CrowdControlManager> list_of_cc;

    public float effect_duration = 6f;
    public float wind_force = 10f;
    public Vector3 wind_direction;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cc_manager = collider.GetComponent<CrowdControlManager>();
            cc_manager.ApplyWindyTunnel(wind_force, wind_direction);

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
            cc_manager.RemoveWindyTunnel();

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
                cc_manager.RemoveWindyTunnel();
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
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
