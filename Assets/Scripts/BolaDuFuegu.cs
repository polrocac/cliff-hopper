using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDuFuegu : MonoBehaviour
{
    public float vel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * vel * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            p.Muelto();
        }
    }


}
