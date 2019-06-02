using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    public enum Items
    {
        None = 0,
        Tier1_Orange,
        Tier1_Lime,
        Tier1_Turquoise,
        Tier1_Fuchsia
    };

    Items item_carrying;

    public void SetItemCarrying(Items item_to_carry)
    {
        item_carrying = item_to_carry;
    }

    public void DropItem()
    {
        item_carrying = Items.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.layer != this.gameObject.layer)
            {
                this.DropItem();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        item_carrying = Items.None;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(item_carrying + " Layer: " + gameObject.layer);
    }
}
