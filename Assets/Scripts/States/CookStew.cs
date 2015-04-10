using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{
    public class CookStew : State<Wife>
    {
        public override void Enter(Wife minersWife)
        {
            if (!minersWife.Cooking)
            {
                Debug.Log(minersWife.ID + "Putting the stew in the oven");
                Message.Dispatch(2, minersWife.ID, minersWife.ID, MessageType.StewsReady);
                minersWife.Cooking = true;
            }
        }

        public override void Execute(Wife minersWife)
        {
            Debug.Log(minersWife.ID + "Fussin' over food");
        }

        public override void Exit(Wife minersWife)
        {
            Debug.Log(minersWife.ID + "Puttin' the stew on the table");
        }

        public override bool OnMessage(Wife minersWife, Telegram telegram)
        {
            switch (telegram.messageType)
            {
                case MessageType.HiHoneyImHome:
                    // Ignored here; handled in WifesGlobalState below
                    return false;
                case MessageType.StewsReady:
                    Debug.Log("Message handled by " + minersWife.ID + " at time ");
                    Debug.Log(minersWife.ID + "StewReady! Lets eat");
                    Message.Dispatch(0, minersWife.ID, minersWife.HusbandID, MessageType.StewsReady);
                    minersWife.Cooking = false;
                    minersWife.StateMachine.ChangeState(new DoHouseWork());
                    return true;
                default:
                    return false;
            }
        }
    }
}
