public class SingleMastedShipScript : ShipScript
{
    private SingleMastedShip ship = new SingleMastedShip();
    public override ShipModel Ship
    {
        get
        {
            return ship;
        }
        set
        {
            ship = (SingleMastedShip)value;
        }
    }
}
