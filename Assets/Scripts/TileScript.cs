using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Material OriginalWater;
    public Material TargetWater;
    public Material BlackWater;
    public Dictionary<string, string> Board = new Dictionary<string, string>();

    void Start()
    {
        //SetupStartBoard();
    }

    // Update is called once per frame
    void Update()
    {   
    }
    public void SetOriginalWater(Renderer target)
    {
        target.sharedMaterial = OriginalWater;
    }
    public void SetTargetWater(Renderer target)
    {
        target.sharedMaterial = TargetWater;
    }
    public void SetBlackWater(Renderer target)
    {
        target.sharedMaterial = BlackWater;
    }
    public void SetMaterial(Renderer target, Material material)
    {
        target.sharedMaterial = material;
    }

    //public void SetupStartBoard()
    //{
    //    for (int i = 1; i <= 240; i++)
    //        Board.Add(i.ToString(), 0);
    //    // DebugConsole:
    //    Debug.Log("FinishedBoardSetup:");
    //    Debug.Log("Board 0,0: " + Board["1"]);
    //    Debug.Log("Board 1,0: " + Board["2"]);
    //}
}
