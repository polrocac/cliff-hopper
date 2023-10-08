using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierra : MonoBehaviour
{
    private List<Vector3> route;

    private int indx, next;

    public float vel;
    public float velAngular;
    private float t;

    // Start is called before the first frame update
    void Awake()
    {
        t = 0;
        indx = 0;
        next = 1;
        vel = vel + vel * Random.Range(-0.5f, 0.5f);
        route = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.right * velAngular * Time.deltaTime);

        transform.position = Vector3.Lerp(route[indx], route[next], t * vel);
        t += Time.deltaTime;

        if (t * vel >= 1)
        {
            t = (t * vel - 1)/vel;
            
            if (indx < next)
            {
                if (++next >= route.Count)
                {
                    next -= 2;
                }
                ++indx;
            }
            else
            {
                if (--next < 0)
                {
                    next += 2;
                }
                --indx;
            }
        }
    }

    public void AddPoint(Vector3 v)
    {
        route.Add(v);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Muelto();
        }
    }
}
