using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Aplastable
{

    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = scale + Random.Range(-0.15f, 0.15f);
        transform.Translate(Vector3.down * (transform.localScale.y - scale) / 2);
        transform.localScale *= scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Muelto();
        }
    }
}
