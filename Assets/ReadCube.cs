using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform tUp;
    public Transform tDown;
    public Transform tFront;
    public Transform tBack;
    public Transform tLeft;
    public Transform tRight;
    
    public List<GameObject> upRays = new List<GameObject>();
    public List<GameObject> downRays = new List<GameObject>();
    public List<GameObject> frontRays = new List<GameObject>();
    public List<GameObject> backRays = new List<GameObject>();
    public List<GameObject> leftRays = new List<GameObject>();
    public List<GameObject> rightRays = new List<GameObject>();

    private int layerMask = 1 << 6;

    CubeState cubeState;
    public GameObject emptyGO;

    void Start()
    {
        SetRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        ReadState();
        CubeState.started = true;
    }

    void Update()
    {
        
    }

    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();

        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);
    }

    void SetRayTransforms()
    {
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));

    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0; 
        List<GameObject> rays = new List<GameObject>();

        // Create 9 rays in the shape of the side of the cube
        for(int y = 1; y > -2; y--)
        {
            for(int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x, 
                                               rayTransform.localPosition.y + y, 
                                               rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if(Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                // print(hit.collider.gameObject.name);
            }
            else 
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }

        return facesHit;
    }
}
