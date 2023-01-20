using UnityEngine;
using System;

using static Enums;

public class SingleMastedShip : ShipModel
{
    //private SingleMastedShip ship = new SingleMastedShip();
    public SingleMastedShip Ship = new SingleMastedShip();

    public SingleMastedShip()
    {
        shipType = ShipType.SingleMastedShip;
        damage = "3";
        armor = "3";
        watchDistance = "4";
        shootDistance = "4";
        Speed = 5;
    }
    //public override ShipModel Ship
    //{
        //get
        //{
            //return this.Ship;
        //}
        //set
        //{
            //try:
            //{
                //ship = (SingleMastedShip)value;
            //}
            //catch (Exception e)
            //{
                //Debug.Log("ERROR in SingleMastedShip SetUp:");
               // Debug.Log(e);
            //}
        //}
    //}
    
    //public SingleMastedShip() : base()
    //{
        //this. shipId = "1";
    //}
}