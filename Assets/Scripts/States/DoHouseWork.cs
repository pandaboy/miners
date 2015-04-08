using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{
    public class DoHouseWork : State<Wife>
    {
        static System.Random rand = new System.Random();

        public override void Enter(Wife minersWife)
        {
            Debug.Log(minersWife.Id + "Time to do some more housework!");
        }

        public override void Execute(Wife minersWife)
        {
            switch (rand.Next(3))
            {
                case 0:
                    Debug.Log(minersWife.Id + "Moppin' the floor");
                    break;
                case 1:
                    Debug.Log(minersWife.Id + "Washin' the dishes");
                    break;
                case 2:
                    Debug.Log(minersWife.Id + "Makin' the bed");
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
