using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;

    private Vector3 offset, targetOffset;
    private Vector3 offsetVel;

    private float currentZoom, targetZoom;
    private float zoomVel;

    private Camera c;

    public float padding = 1;

    Queue<Vector2> importantCorners;
    List<Vector2> cornersList;

    private int cornerIndex = 0;

    private bool final = false;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Camera>();
        playerTransform = GameObject.FindGameObjectWithTag("Guide").transform;
        offset = Vector3.zero;

        cornersList = LevelGenerator.Instance.getCornersPos();
        importantCorners = new Queue<Vector2>();

        UpdateImportantCorners();
        UpdateCameraParams();
        offset = targetOffset;
        setZoom(targetZoom);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!final)
        {
            float xz = (playerTransform.position.x + playerTransform.position.z) / 2 + 15;
            transform.position = new Vector3(xz, playerTransform.position.y + 10, xz) + offset;

            UpdateImportantCorners();
            UpdateCameraParams();

            if (cornerIndex >= cornersList.Count)
            {
                final = true;
                endPos = new Vector3(xz, playerTransform.position.y + 10, xz);
            }
        }
        else
        {
            transform.position = endPos + offset;
        }

        //Debug.Log("Nº Important corners: " + importantCorners.Count);
        //Debug.Log("Corner Index: " + cornerIndex);

        setZoom(Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVel, 1f));
        offset = Vector3.SmoothDamp(offset, targetOffset, ref offsetVel, 1f);

    }

    private readonly static float k = Mathf.Cos(Mathf.PI / 4) * 16 / (9 * 2);
    public void setZoom(float numBloques)
    {
        currentZoom = numBloques;
        c.orthographicSize = (numBloques + padding * 2) * k;
    }

    private void UpdateImportantCorners()
    {
        Vector2 cameraCoords = CoordManager.toCHCoords(this.transform.position);

        while (cornerIndex < cornersList.Count)
        {
            Vector2 nextCorner = cornersList[cornerIndex];
            if (cameraCoords.y - nextCorner.y < 5)
            {
                importantCorners.Enqueue(nextCorner);
                cornerIndex++;
            }
            else break;
        }

        while (importantCorners.Count > 3)
        {
            Vector2 corner = importantCorners.Peek();
            if (cameraCoords.y - corner.y < -3)
            {
                importantCorners.Dequeue();
            }
            else break;
        }
    }

    private void UpdateCameraParams()
    {
        float maxX = Mathf.NegativeInfinity;
        float minX = Mathf.Infinity;
        foreach (Vector2 pos in importantCorners)
        {
            if (pos.x > maxX) maxX = pos.x;
            if (pos.x < minX) minX = pos.x;
        }

        float center = (maxX + minX) / 2;
        float zoom = maxX - minX;

        targetOffset = new Vector3(-center/2, 0, center/2);
        targetZoom = zoom;
    }
}
