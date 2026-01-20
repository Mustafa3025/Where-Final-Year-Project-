using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.UI;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] MazeGeneratorUno mazeGenerator;
    [SerializeField] GameObject MazeCellPrefab;

    public Vector3 mazeOrigin = Vector3.zero;



    public float cellSize = 1f;

    private void Start()
    {
        MazeCell[,] maze = mazeGenerator.GetMaze();

        for(int x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for(int y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                //Instantiating a new cell prefab as a child of the maze rendeerere object
                //GameObject newCell = Instantiate(MazeCellPrefab, new Vector3((float)x * cellSize, 0f, (float)y * cellSize), Quaternion.identity, transform);
                GameObject newCell = Instantiate(MazeCellPrefab, mazeOrigin +   new Vector3((float)x * cellSize, 0f, (float)y * cellSize), Quaternion.identity, transform);

                //getting reference to the cell's mazecellprefab script
                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();
                //Determine which walls are meant to stay active (not be broken/removeded)
                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;


                bool right = false;
                bool bottom = false;

                if (x == mazeGenerator.mazeWidth - 1) right = true;
                if (y == 0) bottom = true;

                mazeCell.Init(top, bottom, right, left);
            }
        }
    }
}
