using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 6;

    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            readCube.ReadState();

            // Raycast from mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left, 
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };

                foreach(List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        cubeState.PickUp(cubeSide);
                        
                        // Start side rotation logic 
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}
