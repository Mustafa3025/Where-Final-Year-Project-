using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneratorold : MonoBehaviour
{

    //_mazeDepth is z-axis
    //_mazeWidth is x-axis
    //_mazeGrid is comprised of both tgt
    [SerializeField]
    private MazeCell _mazeCellPreFab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    //Check th generate mazemethod to understand why the return type of Start ischanged from IEnumarator to void
    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        
        for (int x = 0;x < _mazeWidth; x++)
        {
            for (int z = 0; z< _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPreFab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        //yield return GenerateMaze(null, _mazeGrid[0,0]);
        GenerateMaze(null, _mazeGrid[0, 0]);
    }

    //This method only visists onecell
    //Removing the return type of the method from IEnumerator to void makes the maze load instatly without the slowly loading time thingy
    //The commented code is used to do this maze generation thingy slowly iteratively... were as now it just spawns the entire maze without genreating eachcell block one by one
    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        //yield return new WaitForSeconds(0.05f);


        MazeCell nextCell;

        do
        {
            //This part of code inside the do while was outside without the do while dp hiky and was causing 
            //issues such as stooping the maze genration once the algorithm reached a cell that had no unvisted neighbors..meaning it could stop at like 5% during the creation of the entire maze
            //in like a circular manner  and just stop...
            //var nextCell = GetNextUnvisitedCell(currentCell);
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                //yield return GenerateMaze(currentCell, nextCell);
                GenerateMaze(currentCell, nextCell);
            }

        } while (nextCell != null); 

    }

    //This method goes onto a random unvisited neighboring cell 
    //First thing we do is get all the unvisited neighbors then we can pick one of these at random
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetNextUnvisitedCells(currentCell);
        //The FUCKING ERROR!!
        //The error was that I accidentally added "IEnumerator in the method name" --> private IEnumatrator GetNexxtUnvisited yadayadada... 
        //return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }
    
    private IEnumerable<MazeCell> GetNextUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        //Checking if there is an unvisited cell to the right
        //and then returining the cell back
        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        //Checking if there is an unvisited cell to theleft
        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            
            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }

        }

        //Checking the front
        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        //Checking the back
        if (z - 1 >= 0)
        {
            var celLToBack = _mazeGrid[x, z - 1];

            if (celLToBack.IsVisited == false)
            {
                yield return celLToBack;
            }

        }


    }



    
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        // Checking if the position of the previouscell is on the left of the current cell
        // This allows the algorithm to remove the overlapping walls to create a pathway
        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.clearRightWall();
            currentCell.clearLeftWall();
            return;
        }

        //Same thing as above but instead here wecheck the if the algorithm as gone from right to left to clear the relavent walls
        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {

            previousCell.clearLeftWall();
            currentCell.clearRightWall();
            return;
        }

        //Now here we will check the back to front path
        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.clearFrontWall();
            currentCell.clearBackWall();
            return;
        }

        //Last one ...  front to back
        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {

            previousCell.clearBackWall();   
            currentCell.clearFrontWall();
            return;
        }
    }




    void Update()
    {
        
    }
}
