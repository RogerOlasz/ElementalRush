using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterPathBehaviour : MonoBehaviourPun, IPunObservable
{
    public float effect_duration = 10f;

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
