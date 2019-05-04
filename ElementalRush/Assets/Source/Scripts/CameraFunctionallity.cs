using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctionallity : MonoBehaviour
{
    public GameObject player;
    private Vector3 dif;

    // Start is called before the first frame update
    void Start()
    {
        dif = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + dif;
    }
}
