using UnityEngine;

public class PirateScript : MonoBehaviour
{
    public PirateModel Pirate { get; set; }

    public void Awake()
    {
        this.Pirate = new PirateModel();
    }

    public void ChangeShipNumber(string num)
    {
        this.Pirate.shipId = num;
    }
}
