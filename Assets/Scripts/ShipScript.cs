using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public ShipModel Ship { get; set; }
    public void Awake()
    {
        this.Ship = new ShipModel();
    }
}
