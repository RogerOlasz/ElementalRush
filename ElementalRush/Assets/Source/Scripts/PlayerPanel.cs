using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public Text player_status;
    public Image player_element_energy;

    private Vector2 real_position;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        my_player = (GameObject)photonView.Owner.TagObject;

        my_rect = GetComponent<RectTransform>();
        player_camera = Camera.main;
        canvas = transform.parent.GetComponent<Canvas>();

        player_status = transform.Find("PlayerElementText").GetComponent<Text>();
        player_element_energy = transform.Find("ElementEnergyBarBackground").gameObject.transform.Find("ElementEnergyBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Vector2 position_on_screen = player_camera.WorldToScreenPoint(my_player.transform.position);

            Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
            my_rect.anchoredPosition = final_position;
        }
        else
        {
            Vector2 position_on_screen = player_camera.WorldToScreenPoint(PhotonView.Find((photonView.ViewID - 1)).gameObject.transform.position);

            Vector2 final_position = new Vector2((position_on_screen.x / canvas.scaleFactor) + x_offset, (position_on_screen.y / canvas.scaleFactor) + y_offset);
            my_rect.anchoredPosition = Vector2.Lerp(final_position, real_position, Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player_status.text);
            stream.SendNext(player_element_energy.fillAmount);

            stream.SendNext(my_rect.anchoredPosition);
        }
        else if (stream.IsReading)
        {
            player_status.text = (string)stream.ReceiveNext();
            player_element_energy.fillAmount = (float)stream.ReceiveNext();

            real_position = (Vector2)stream.ReceiveNext();
        }
    }
}
