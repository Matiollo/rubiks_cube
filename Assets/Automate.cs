using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>() {};
    private readonly List<string> allMoves = new List<string>() 
        {  
            "U", "D", "L", "R", "F", "B",
            "U2", "D2", "L2", "R2", "F2", "B2",
            "U'", "D'", "L'", "R'", "F'", "B'"
        };

    private CubeState cubeState;
    private ReadCube readCube;
    
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
    }

    void Update()
    {
        if(moveList.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            DoMove(moveList[0]);

            moveList.Remove(moveList[0]);
        }
    }

    public void Shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(10, 30);

        for(int i = 0; i < shuffleLength; i++)
        {
            int randonMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randonMove]);
        }

        moveList = moves;
    }

    void DoMove(string move)
    {
        readCube.ReadState();
        CubeState.autoRotating = true;
        if(move == "U") 
        {
            RotateSide(cubeState.up, -90);

        }
        else if(move == "U'") 
        {
            RotateSide(cubeState.up, 90);

        }
        else if(move == "U2") 
        {
            RotateSide(cubeState.up, -180);

        }
        if(move == "D") 
        {
            RotateSide(cubeState.down, -90);

        }
        else if(move == "D'") 
        {
            RotateSide(cubeState.down, 90);

        }
        else if(move == "D2") 
        {
            RotateSide(cubeState.down, -180);

        }
        if(move == "L") 
        {
            RotateSide(cubeState.left, -90);

        }
        else if(move == "L'") 
        {
            RotateSide(cubeState.left, 90);

        }
        else if(move == "L2") 
        {
            RotateSide(cubeState.left, -180);

        }
        if(move == "R") 
        {
            RotateSide(cubeState.right, -90);

        }
        else if(move == "R'") 
        {
            RotateSide(cubeState.right, 90);

        }
        else if(move == "R2") 
        {
            RotateSide(cubeState.right, -180);

        }
        if(move == "F") 
        {
            RotateSide(cubeState.front, -90);

        }
        else if(move == "F'") 
        {
            RotateSide(cubeState.front, 90);

        }
        else if(move == "F2") 
        {
            RotateSide(cubeState.front, -180);

        }
        if(move == "B") 
        {
            RotateSide(cubeState.back, -90);

        }
        else if(move == "B'") 
        {
            RotateSide(cubeState.back, 90);

        }
        else if(move == "B2") 
        {
            RotateSide(cubeState.back, -180);

        }
    }

    void RotateSide(List<GameObject> side, float angle) 
    {
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }
}
