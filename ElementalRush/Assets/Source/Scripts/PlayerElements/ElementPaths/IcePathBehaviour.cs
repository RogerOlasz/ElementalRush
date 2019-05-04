using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePathBehaviour : MonoBehaviour
{
    public float effect_duration = 12f;

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
}
