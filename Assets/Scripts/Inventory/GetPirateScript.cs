using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPirateScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void GetPirate()
    {
        Transform tf = GameObject.Find("Content").transform;
        GameObject newPirate = Instantiate(prefab, tf);
        newPirate.name = "Szergej";
    }
}
