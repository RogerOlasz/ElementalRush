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
            Vector2 position_on_screen = player_camera.WorldToScreenPoint(PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position);

            Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
            my_rect.anchoredPosition = final_position;
        }
        else
        {
            //Vector3 player_pos = PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position;
            //Vector2 position_on_screen = Camera.main.WorldToScreenPoint(player_pos);

            //Vector2 final_position = new Vector2((position_on_screen.x / transform.parent.GetComponent<Canvas>().scaleFactor) + x_offset, (position_on_screen.y / transform.parent.GetComponent<Canvas>().scaleFactor) + y_offset);
            //this.GetComponent<RectTransform>().anchoredPosition = final_position;
            //Debug.Log(Camera.main.transform.position);

            Vector2 position_on_screen = Camera.main.WorldToViewportPoint(PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position);

            Debug.Log(position_on_screen);
            position_on_screen.y += 0.075f;

            this.GetComponent<RectTransform>().anchorMin = position_on_screen;
            this.GetComponent<RectTransform>().anchorMax = position_on_screen;
        }
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
