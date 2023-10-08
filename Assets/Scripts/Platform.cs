using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bioma
{
    GRASS, DESERT, ICE, FIRE
}

public class Platform : MonoBehaviour
{
    protected Transform terrainTransf;

    private GameObject basemodel, topmodel;
    private Bioma _bioma;
    public Bioma Bioma { 
        get {
            return _bioma; 
        } 
        set {
            _bioma = value;
            foreach (Transform child in terrainTransf)
            {
                if (child.name != value.ToString())
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                    basemodel = child.gameObject;
                }
            }
        }
    }

    private Trampas _trampa;
    public Trampas Trampa
    {
        get
        {
            return _trampa;
        }
        set
        {
            _trampa = value;

            if (value.ToString() == "SIERRA")
            {
                transform.Find("guiaSierra").gameObject.SetActive(true);
                value = Trampas.NORMAL;
            }

            foreach (Transform child in basemodel.transform)
            {
                if (child.name == value.ToString() || child.name == "default")
                {
                    topmodel = child.gameObject;
                    child.gameObject.SetActive(true);
                    if (value.ToString() == "CORNER") {
                        transform.Find("CCol").gameObject.SetActive(true);
                    }
                    else if (value.ToString() == "LENTO")
                    {
                        transform.Find("LCol").gameObject.SetActive(true);
                    }
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        terrainTransf = transform.Find("Terrain");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetHeight(float h)
    {
        terrainTransf.localScale = new Vector3(terrainTransf.localScale.x, terrainTransf.localScale.x * h, terrainTransf.localScale.z);
        terrainTransf.Translate(0, -(1 - h)/2, 0);
    }

    public void setGlow(bool b)
    {
        if (b) topmodel.transform.Find("top").GetComponent<Renderer>().material.EnableKeyword("_EMISSION"); 
        else topmodel.transform.Find("top").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }


}
