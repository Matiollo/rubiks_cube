using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    private CubeState cubeState;
    private ReadCube readCube;

    private bool doOnce = true;

    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
    }

    void Update()
    {
        if(CubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {
        readCube.ReadState();

        // Convert state of the cube into a string
        string moveString = cubeState.GetStateString();

        // Solve Rubic's cube using Kociemba package
        string info = "";

        // For first time only
        // string solution = SearchRunTime.solution(moveString, out info, buildTables: true);
        
        // For second+ time
        string solution = Search.solution(moveString, out info);


        // Convert string to a list
        List<string> solutionList = StringToList(solution);

        // Automate the list
        Automate.moveList = solutionList;
        
        print(info);
    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
