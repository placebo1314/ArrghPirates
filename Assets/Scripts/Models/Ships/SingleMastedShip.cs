using static Enums;

public class SingleMastedShip : ShipModel
{
    public SingleMastedShip()
    {
        shipType = ShipType.SingleMastedShip;
        damage = "3";
        armor = "3";
        watchDistance = "4";
        shootDistance = "4";
        Speed = 5;
    }
}
