using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [System.NonSerialized]
    public float velHorizontal;

    private GameObject player;

    public void Init(GameObject player)
    {
        this.player = player;
    }

    void FixedUpdate()
    {
        // Sincronizar posición x,z con la del player
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);


        //transform.position += new Vector3(1 - direction, 0, direction) * velHorizontal * Time.fixedDeltaTime;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Rampa"/* && hit.distance*/)
            {
                //Debug.Log("Rayo vallecano: " + hit.distance);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
                transform.position += Vector3.down * (hit.distance - transform.localScale.y/2);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            }
        }
    }
}
