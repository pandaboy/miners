using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{
    public class WifesGlobalState : State<Wife>
    {
        static System.Random rand = new System.Random();

        public override void Enter(Wife minersWife)
        {

        }

        public override void Execute(Wife minersWife)
        {
            if (rand.Next(10) == 1 && !minersWife.StateMachine.IsInState(new VisitBathroom()))
            {
                minersWife.StateMachine.ChangeState(new VisitBathroom());
            }
        }

        public override void Exit(Wife minersWife)
        {

        }

        public override bool OnMessage(Wife minersWife, Telegram telegram)
        {
            switch (telegram.messageType)
            {
                case MessageType.HiHoneyImHome:
                    Debug.Log("Message handled by " + minersWife.ID + " at time ");
                    Debug.Log(minersWife.ID + "Hi honey. Let me make you some of mah fine country stew");
                    minersWife.StateMachine.ChangeState(new CookStew());
                    return true;
                case MessageType.StewsReady:
                    return false;
                default:
                    return false;
            }
        }
    }
}
