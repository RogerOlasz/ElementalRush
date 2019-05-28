using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public PlayfabManager playfab_manager;

    public GameObject map_manager;
    private MapManager map_manager_script;    

    // Start is called before the first frame update
    void Start()
    {
        map_manager_script = map_manager.GetComponent<MapManager>();
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
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;

        GameObject player = PhotonNetwork.Instantiate("Player", map_manager_script.blue_spawn_points[0].transform.position, Quaternion.identity, 0);
        player.layer = LayerMask.NameToLayer("TeamBlue");

        //if(PhotonNetwork.PlayerList.Length % 2 != 0)
        //{
        //    GameObject player = PhotonNetwork.Instantiate("Player", map_manager_script.blue_spawn_points[PhotonNetwork.PlayerList.Length - 1].transform.position, Quaternion.identity, 0);
        //    player.layer = LayerMask.NameToLayer("TeamBlue");     
        //}
        //else
        //{
        //    GameObject player = PhotonNetwork.Instantiate("Player", map_manager_script.red_spawn_points[PhotonNetwork.PlayerList.Length -2].transform.position, Quaternion.identity, 0);
        //    player.layer = LayerMask.NameToLayer("TeamRed");
        //}   
    }
}
