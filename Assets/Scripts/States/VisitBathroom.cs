using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{
    public class VisitBathroom : State<Wife>
    {
        public override void Enter(Wife minersWife)
        {
            Debug.Log(minersWife.Id + "Walkin' to the can. Need to powda mah pretty li'lle nose");
        }

        public override void Execute(Wife minersWife)
        {
            Debug.Log(minersWife.Id + "Ahhhhhh! Sweet relief!");
            minersWife.StateMachine.RevertToPreviousState();  // this completes the state blip
        }

        public override void Exit(Wife minersWife)
        {
            Debug.Log(minersWife.Id + "Leavin' the Jon");
        }

        public override bool OnMessage(Wife minersWife, Telegram telegram)
        {
            return false;
        }
    }
}
