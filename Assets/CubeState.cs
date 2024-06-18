using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();

    public static bool autoRotating = false;
    public static bool started = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach(GameObject face in cubeSide)
        {
            // Attach all the faces of the cube side as children to the central face
            if(face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
    }

    public void PutDown(List<GameObject> cubeSide, Transform pivot)
    {
        foreach(GameObject face in cubeSide)
        {
            if(face != cubeSide[4])
            {
                face.transform.parent.transform.parent = pivot;
            }
        }
    }

    string GetSideString(List<GameObject> side)
    {
        string sideString = "";

        foreach(GameObject face in side)
        {
            // sideString += face.name[0].ToString();
            if(face.name[0] == 'G')
            {
                sideString += "U";
            }
            else if(face.name[0] == 'R')
            {
                sideString += "R";
            }
            else if(face.name[0] == 'Y')
            {
                sideString += "F";
            }
            else if(face.name[0] == 'O')
            {
                sideString += "D";
            }
            else if(face.name[0] == 'B')
            {
                sideString += "L";
            }
            else if(face.name[0] == 'W')
            {
                sideString += "B";
            }
        }

        return sideString;
    }

    public string GetStateString()
    {
        string stateString = "";
        stateString += GetSideString(up);
        stateString += GetSideString(right);
        stateString += GetSideString(front);
        stateString += GetSideString(down);
        stateString += GetSideString(left);
        stateString += GetSideString(back);
        return stateString;
    }
}
