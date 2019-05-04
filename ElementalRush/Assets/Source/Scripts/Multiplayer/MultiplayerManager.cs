using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPun
{
    public PlayfabManager playfab_manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {

    }

    public void CreateOrJoin()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        RoomOptions new_room = new RoomOptions();
        new_room.MaxPlayers = 6;
        new_room.IsVisible = true;

        int random_id = Random.Range(0, 3000);

        PhotonNetwork.CreateRoom("Dev: " + random_id, new_room, TypedLobby.Default);
    }

    public virtual void OnJoinedRoom()
    {  
        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
    }
}
