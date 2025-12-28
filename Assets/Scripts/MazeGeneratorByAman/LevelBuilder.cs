using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelBuilder : MonoBehaviour
{

    
    //Working Booleans
    bool makingLevel;
    bool makingChain; //Single line we iterate through

    [SerializeField] Tile startRoomType;
    [SerializeField] Tile endRoomType;
    [SerializeField] Tile[] corridorTypes;
    [SerializeField] Tile[] roomTypes;

    List<Vector2> tileCoordinates = new List<Vector2>();
    List<Tile> placedTiles = new List<Tile>();

    public void Start()
    {
        NewLevel();
    }

    public void NewLevel()
    {
        //Clear Level // Restting
        if (!makingLevel)
        {
           // StartCoroutine(CrouteMakeLevel());
        }
    }

    IEnumerator CrouteMakeLevel()
    {
        makingLevel = true; 
        //Vector3 .. middle of the map
        //Qaurtenion Identiot for default 0,0,0 rotation
        //This transforms this parented to  child smth smth... 

        Tile startTile = Instantiate(startRoomType, Vector3.zero, Quaternion.identity, transform);
        yield return null;
    }

    //making the makinglevel fucntion in an update function but not on a ccoroutine , even though you can...
}
