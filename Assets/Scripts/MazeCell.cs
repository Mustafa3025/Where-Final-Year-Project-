using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;


    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _unvisitedBlock;

    //Boolean propert to check if the cell has been vivsted by the maze generator
    public bool IsVisited { get; private set; }
    
    //Helpermethods
    //This will becall when thecell be visited by the cell generator algorthm
    //Also deactivating the unvisible walls sothey become visible
    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);

    }

    //Helper methods to clear each wall
    public void clearLeftWall()
    {
        _leftWall.SetActive(false);  
    }

    public void clearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void clearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void clearBackWall()
    {
        _backWall.SetActive(false);
    }

}
