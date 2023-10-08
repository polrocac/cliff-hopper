using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private Transform guia;

    private Color targetColor;
    private Color initialColor;
    private Material material;
    float time;
    float transitionTime;

    // Start is called before the first frame update
    void Start()
    {
        guia = GameObject.FindGameObjectWithTag("Guide").transform;

        material = GetComponent<MeshRenderer>().material;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = guia.position + Vector3.down * 1;
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.Lerp(initialColor, targetColor, time/transitionTime));
        time += Time.deltaTime;

    }

    public void ChangeFogColor(Color c, float transitionTime)
    {
        if(transitionTime == 0)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", c);
            initialColor = c;
            targetColor = c;
            return;
        }
        initialColor = targetColor;
        targetColor = c;
        this.transitionTime = transitionTime;
        this.time = 0;
    }
}
