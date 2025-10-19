using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SelectTarget selectTarget;
    public TileScript tileScript;
    // Start is called before the first frame update
    void Start()
    {
        // DebugConsole:
        Debug.Log("Manager start ");

        if (tileScript != null)
        {
            tileScript.SetupStartBoard(BoardLayouts.BasicFleet);
        }
        else
        {
            Debug.LogWarning("TileScript reference missing on GameManager; the board will stay empty.");
        }

        StartCoroutine(selectTarget.Shoot());
        Debug.Log("Manager end ");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
