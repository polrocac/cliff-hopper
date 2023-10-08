using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilar : MonoBehaviour
{

    private Bioma _bioma;
    public Bioma Bioma
    {
        get
        {
            return _bioma;
        }
        set
        {
            _bioma = value;
            foreach (Transform child in transform)
            {
                if (child.name != value.ToString())
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
