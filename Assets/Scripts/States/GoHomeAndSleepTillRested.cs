using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{

    // In this state, the miner goes home and sleeps
    public class GoHomeAndSleepTillRested : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + "Walkin' Home");
            miner.MinerLocation = Location.home;
            Message.Dispatch(0, miner.ID, miner.WifeID, MessageType.HiHoneyImHome);
        }

        public override void Execute(Miner miner)
        {
            if (miner.HowFatigued < miner.TirednessThreshold)
            {
                Debug.Log(miner.ID + "All mah fatigue has drained away. Time to find more gold!");
                miner.StateMachine.ChangeState(new EnterMineAndDigForNugget());
            }
            else
            {
                miner.HowFatigued--;
                Debug.Log(miner.ID + "ZZZZZ....");
            }
        }

        public override void Exit(Miner miner)
        {

        }

        public override bool OnMessage(Miner miner, Telegram telegram)
        {
            switch (telegram.messageType)
            {
                case MessageType.HiHoneyImHome:
                    return false;
                case MessageType.StewsReady:
                    Debug.Log("Message handled by " + miner.ID + " at time ");
                    Debug.Log(miner.ID + "Okay Hun, ahm a comin'!");
                    miner.StateMachine.ChangeState(new EatStew());
                    return true;
                default:
                    return false;
            }
        }
    }
}
