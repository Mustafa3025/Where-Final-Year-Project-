using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO.Abstractions;

public class MazeGeneratorUno : MonoBehaviour
{
    [Range(5, 100)]
    public int mazeWidth = 5, mazeHeight = 5; // dimensions of the maze
    public int startX, startY; //Where the maze algo will start from
    MazeCell[,] maze; //array of maze cell (maze itself)
    Vector2Int currentCell; //maze cell we are currently at

    public MazeCell[,] GetMaze()
    {
        maze = new MazeCell[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }
        }

        CarvePath(startX, startY);
        return maze;

    }

    List<Direction> directions = new List<Direction>
    {
        Direction.Up, Direction.Down, Direction.Left, Direction.Right,
    };

    List<Direction> GetRandomDirections()
    {
            // Make a copy of our direction list that we can mess around with
        List<Direction> dir = new List<Direction>(directions);

        // Make a directions list to put our randomized directions into
        List<Direction> rndDir = new List<Direction>();

        while (dir.Count > 0) //Looping until our random diections list is empty
        {
            int rnd = Random.Range(0, dir.Count); //getting random index in list
            rndDir.Add(dir[rnd]);  // adding the random direction to our list
            dir.RemoveAt(rnd); //removing that direction so that we can't choose it again
        }

        return rndDir;
    }
     

    bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x, y].visited) return false;
        else return true;
    }

    Vector2Int CheckNeighbours()
    {
        List<Direction> rndDir = GetRandomDirections();

        for(int i = 0; i <rndDir.Count; i++) 
        {
            Vector2Int neighbour = currentCell; //Setting neighbour coordinates to current cell

            switch (rndDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                
                case Direction.Down:
                    neighbour.y--;
                    break;
                
                case Direction.Right:
                    neighbour.x++;
                    break;
                
                case Direction.Left:
                    neighbour.x--;
                    break;
                
            }
            // if the neighbour we jsut tried is valid, we can return that neighbour, If not we go again.
            if(IsCellValid(neighbour.x, neighbour.y)) return neighbour;

        }
        // if we have yet to recieve a nieghbour then that means we have hit a dead end, meaning no valid neighbours.. end of maze
        return currentCell;
    }

    //takes two maze positions and sets the cells accordingly
    void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
         // we can only go in one direction at a time so we can handle this using if else statements;
        if(primaryCell.x > secondaryCell.x)
        {
            //primary cell is to the right of the secondary cell .. meaning primary cell's left wall needs to be broken
            maze[primaryCell.x, primaryCell.y].leftWall = false;
        } 

        else if(primaryCell.x < secondaryCell.x)
        {   // Here it's the opposited where the secondary cell is to the right of the primary cell hence the secondary cell's left wall is to be broken
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        }

       
        if (primaryCell.y < secondaryCell.y)
        {
            //primary cell is below the secondary cell, hence primary cell's top wall needs to be removed
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }

        else if (primaryCell.y > secondaryCell.y)
        {
            //secondary cell is below the primary cell, hence secondary cell's top wall needs to be removed
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }

    }

    void CarvePath(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
        {
            x = y = 0;
            Debug.LogWarning("Starting Position is out of bounds, defaulting to 0, 0");
        }

        currentCell = new Vector2Int(x, y);
        //recursive backtracking path
        List<Vector2Int> path = new List<Vector2Int>();

        //Loop until we hit a dead end
        bool deadEnd = false;
        while (!deadEnd)
        { 
            //Get the cell we are going to try
            Vector2Int nextCell = CheckNeighbours();
            //If that cell has no valid arguments we set deadend to true and  break out of the loop
            if(nextCell == currentCell)
            {
                // if that cell has no valid neighbours, set deadend to true so we break out of the loop
                //Backtraicking our previous path
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    // if we find a valid neighbour, break out of the loop
                    if (nextCell != currentCell) break;

                }

                if (nextCell == currentCell) deadEnd = true; 
            }
            else
            {
                BreakWalls(currentCell, nextCell); //set wall flags on these two cells
                maze[currentCell.x, currentCell.y].visited = true;   //set cell to visited before moving on
                currentCell = nextCell; //set the current cell to the valid neighbour we found
                path.Add(currentCell); //Add this cell to our path

            }
        }
    }

}



public enum Direction
{
    Up, Down, Left, Right
}

public class MazeCell
{
    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall;

    // Returning x & y as   Vector2Int for convience sake
    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    // Constructor for the maze, gonna pass in values to generate it
    public MazeCell(int x, int y)
    {
        // The coordinates of this cell in the maze grid
        this.x = x;
        this.y = y;
        // whether the algorithm has visited this cell or not (false or not)
        visited = false;
        // all walls are going to be true until we decide otherwise.. so before sw start its gonna be a cubicles without no way in or out
        topWall = leftWall = true;

    }

}
