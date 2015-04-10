using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace FSM.States
{
    public class EatStew : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + "Smells Reaaal goood Elsa!");
        }

        public override void Execute(Miner miner)
        {
            Debug.Log(miner.ID + "Tastes real good too!");
            miner.StateMachine.RevertToPreviousState();
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + "Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }
    }
}
