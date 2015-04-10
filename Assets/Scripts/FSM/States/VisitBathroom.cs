using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace FSM.States
{
    public class VisitBathroom : State<Wife>
    {
        public override void Enter(Wife minersWife)
        {
            Debug.Log(minersWife.ID + "Walkin' to the can. Need to powda mah pretty li'lle nose");
        }

        public override void Execute(Wife minersWife)
        {
            Debug.Log(minersWife.ID + "Ahhhhhh! Sweet relief!");
            minersWife.StateMachine.RevertToPreviousState();  // this completes the state blip
        }

        public override void Exit(Wife minersWife)
        {
            Debug.Log(minersWife.ID + "Leavin' the Jon");
        }

        public override bool OnMessage(Wife minersWife, Telegram telegram)
        {
            return false;
        }
    }
}
