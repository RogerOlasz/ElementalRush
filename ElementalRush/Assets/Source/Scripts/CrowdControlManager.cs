using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CrowdControlManager : MonoBehaviourPun, IPunObservable
{
    Player my_player_script;

    private float no_cc_movement_speed;

    // Start is called before the first frame update
    void Start()
    {
        my_player_script = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CCSlow(float slow_percentage)
    {
        no_cc_movement_speed = my_player_script.movement_speed;


    }

    void RestoreCCSlow()
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
