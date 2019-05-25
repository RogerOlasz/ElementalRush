using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPunCallbacks
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

    private void FixedUpdate()
    {
        //Debug.Log(PhotonNetwork.NetworkClientState);
    }

    public void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();                
    }

    //public void CreateOrJoin()
    //{
    //    PhotonNetwork.JoinRandomRoom();
    //}

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions new_room = new RoomOptions();
        new_room.MaxPlayers = 6;
        new_room.IsVisible = true;
        //new_room.PublishUserId = true;

        int random_id = Random.Range(0, 3000);

        PhotonNetwork.CreateRoom("Dev: " + random_id, new_room, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(2, 1, 2), Quaternion.identity, 0);
        player.layer = LayerMask.NameToLayer("TeamBlue");        
    }
}
