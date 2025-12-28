using Unity.VisualScripting;
using UnityEngine;

public class VoidFallingScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
