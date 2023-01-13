public class ThreeMastedShipScript : ShipScript
{
    private ThreeMastedShip ship = new ThreeMastedShip();
    public override ShipModel Ship
    {
        get
        {
            return ship;
        }
        set
        {
            ship = (ThreeMastedShip)value;
        }
    }
}