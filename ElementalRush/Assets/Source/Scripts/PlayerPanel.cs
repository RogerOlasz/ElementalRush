using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPanel : MonoBehaviourPun, IPunObservable
{
    public GameObject my_player;
    public float y_offset;
    public float x_offset;

    RectTransform my_rect;
    Canvas canvas;
    Camera player_camera;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        my_player = (GameObject)photonView.Owner.TagObject;

        if (photonView.IsMine)
        {
            my_rect = GetComponent<RectTransform>();
            player_camera = Camera.main;
            canvas = transform.parent.GetComponent<Canvas>();
            

            //my_rect.anchoredPosition = new Vector3(0, 25, 0);
            //Debug.Log("Screen pos: " + Camera.main.WorldToScreenPoint(my_player.transform.position));
            //Debug.Log("World pos: " + my_player.transform.position);
        }        
    }

    // Update is called once per frame
    void Update()
    {        
        if (photonView.IsMine)
        {

        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Debug.Log("Is Mine: " + photonView.ViewID);
            Vector2 position_on_screen = player_camera.WorldToScreenPoint(PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position);

            Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
            my_rect.anchoredPosition = final_position;
        }
        else
        {
            Debug.Log("Else: " + photonView.ViewID);
            //Debug.Log(PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position);
            Vector3 player_pos = PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position;
            Vector2 position_on_screen = player_camera.WorldToScreenPoint(player_pos);
            //Debug.Log(position_on_screen);

            Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
            my_rect.anchoredPosition = final_position;
        }
        //Debug.Log("Player Panel ID: " + photonView.ViewID + "\nPlayer position:" + my_player.transform.position);
        //photonView.RPC("Test", RpcTarget.All, photonView.ViewID);
    }

    //[PunRPC]
    //void Test(PhotonMessageInfo info)
    //{
    //    //Vector2 position_on_screen = player_camera.WorldToScreenPoint(my_player.transform.position);
    //    my_player = (GameObject)info.photonView.Owner.TagObject;
        
    //    Vector2 position_on_screen = player_camera.WorldToScreenPoint(my_player.transform.position);

    //    Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
    //    my_rect.anchoredPosition = final_position;
    //}

    [PunRPC]
    void Test(int sender_view, PhotonMessageInfo info)
    {
        //Debug.Log("Sender: " + info.Sender + "\nPhotonView: " + info.photonView);
        Debug.Log(sender_view);

        //my_player = PhotonView.Find(sender_view).gameObject;
        //
        //Vector2 position_on_screen = player_camera.WorldToScreenPoint(my_player.transform.position);
        //
        //Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
        //my_rect.anchoredPosition = final_position;
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
