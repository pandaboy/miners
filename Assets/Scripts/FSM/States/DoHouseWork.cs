using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace FSM.States
{
    public class DoHouseWork : State<Wife>
    {
        static System.Random rand = new System.Random();

        public override void Enter(Wife minersWife)
        {
            Debug.Log(minersWife.ID + "Time to do some more housework!");
        }

        public override void Execute(Wife minersWife)
        {
            switch (rand.Next(3))
            {
                case 0:
                    Debug.Log(minersWife.ID + "Moppin' the floor");
                    break;
                case 1:
                    Debug.Log(minersWife.ID + "Washin' the dishes");
                    break;
                case 2:
                    Debug.Log(minersWife.ID + "Makin' the bed");
                    break;
                default:
                    break;
            }
        }

        public override void Exit(Wife minersWife)
        {

        }

        public override bool OnMessage(Wife minersWife, Telegram telegram)
        {
            return false;
        }
    }
}
