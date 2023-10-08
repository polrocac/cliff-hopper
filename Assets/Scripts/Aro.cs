using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aro : MonoBehaviour
{
    private GameObject fuego;

    public int MaxDistActivation { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        fuego = transform.Find("boula_du_fuegu").gameObject;

        BoxCollider c = GetComponent<BoxCollider>();
        c.center += Vector3.forward * Random.Range(Mathf.Max(2, MaxDistActivation - 2), MaxDistActivation);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fuego.gameObject.SetActive(true);
            SoundManager.Instance.SelectAudio(5, 0.5f);
        }
    }


}
