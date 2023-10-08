using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boula : MonoBehaviour
{
    private List<Vector3> route;
    private int index;

    private float rotationSpeed = 90 * 0.45f;

    private bool isRotating = false;

    private Rigidbody rg;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        route = LevelGenerator.Instance.getBoulaRoute();

        //Solo se ejecuta 1 vez, ponerlo en el start?
        if (!isRotating)
        {
            isRotating = true;
            SetRotationParams();
        }
    }


    private Vector3 direction;
    private Vector3 rotationCenter;
    private Vector3 axis;

    private float remainingAngle = 90;
    // Update is called once per frame
    void Update()
    {
        if (index >= route.Count) return;


        transform.RotateAround(rotationCenter, axis, rotationSpeed * Time.deltaTime);
        remainingAngle -= rotationSpeed * Time.deltaTime;
        if (remainingAngle <= 0)
        {
            transform.RotateAround(rotationCenter, axis, remainingAngle);

            if (index + 1 >= route.Count)
            {
                index++;
                rg.useGravity = true;
                return;
            }

            SetRotationParams();
            transform.RotateAround(rotationCenter, axis, -remainingAngle);
            remainingAngle = 90 + remainingAngle;
        }

        Debug.DrawLine(rotationCenter + axis*3, rotationCenter - axis*3);
    }

    private void SetRotationParams()
    {
        direction = Vector3.Scale(route[index + 1] - route[index], new Vector3(1, 0, 1)); //Eliminar componente Y
        //direction = new Vector3(1, 0, 0);

        float salto = (route[index + 1] - route[index]).y;
        float radio = salto / 2;
        index++;

        //Vector3 rotationCenter = transform.position + Vector3.Scale(direction + Vector3.down, GetComponent<Renderer>().bounds.size) / 2;
        rotationCenter = transform.position + (direction + Vector3.down) * 0.5f + (Vector3.up + direction) * radio;
        axis = Vector3.Cross(Vector3.up, direction);

        //Debug.Log("Rotation center: " + rotationCenter);
        //Debug.Log("Axis: " + axis);
    }
    //private Vector3 normalizeRotation(Vector3 rotation)
    //{
    //    if (rotation.y > 0)
    //    {
    //        rotation.x = rotation.y - rotation.x;
    //        rotation.y = 0;
    //        rotation.z = rotation.y - rotation.z;
    //    }

    //    if (rotation.x < 0) rotation.x += 360;
    //    if (rotation.y < 0) rotation.y += 360;
    //    if (rotation.z < 0) rotation.z += 360;

    //    return rotation;
    //}

    public void SetSpeed(float speed)
    {
        rotationSpeed = 90 * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Aplastar(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Aplastar(collision.collider);
    }

    void Aplastar(Collider c)
    {
        if (c.CompareTag("Player") || c.CompareTag("Pinguin"))
        {
            Aplastable a = c.GetComponent<Aplastable>();
            a.Aplastar(0.05f);

        }
    }
}
