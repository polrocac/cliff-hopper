using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    Platform platform;

    private void Start()
    {
        platform = GetComponentInParent<Platform>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            p.EnterCorner(platform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float diffX = other.transform.position.x - transform.position.x;
            float diffZ = other.transform.position.z - transform.position.z;

            if (Mathf.Abs(diffX) < 0.5f && Mathf.Abs(diffZ) < 0.5f) return;

            Player p = other.GetComponent<Player>();
            p.ExitCorner(platform);
        }
    }


}
