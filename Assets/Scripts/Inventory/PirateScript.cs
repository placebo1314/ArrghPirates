using UnityEngine;

public class PirateScript : MonoBehaviour
{
    [SerializeField] public string pirateName;
    [SerializeField] public string special;
    [SerializeField] public string damage;
    [SerializeField] public string armor;
    [SerializeField] public string watchDistance;
    [SerializeField] public string shootDistance;
    [SerializeField] public string shipNumber;

    public void ChangeShipNumber(string num)
    {
        this.shipNumber = num;
    }
    public PirateModel GetPirate()
    {
        return new PirateModel()
        {
            pirateName = this.name,
            special = this.special,
            damage = this.damage,
            armor = this.armor,
            watchDistance = this.watchDistance,
            shootDistance = this.shootDistance,
            shipNumber = this.shipNumber
        };
    }
}
