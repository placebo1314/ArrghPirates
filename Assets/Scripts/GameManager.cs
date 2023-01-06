using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SelectTarget selectTarget;
    // Start is called before the first frame update
    void Start()
    {
        // DebugConsole:
        Debug.Log("Manager start ");
        StartCoroutine(selectTarget.Shoot());
        Debug.Log("Manager end ");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
