using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace FSM.States
{
    public class EnterMineAndDigForNugget : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + "Walkin' to the goldmine");
            miner.MinerLocation = Location.mine;
        }

        public override void Execute(Miner miner)
        {
            miner.GoldCarrying += 1;
            miner.HowFatigued += 1;
            Debug.Log(miner.ID + "Pickin' up a nugget");
            
            if (miner.PocketsFull()) {
                miner.StateMachine.ChangeState(new VisitBankAndDepositGold());
            }

            if (miner.Thirsty()) {
                miner.StateMachine.ChangeState(new QuenchThirst());
            }
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + "Ah'm leaving the gold mine with mah pockets full o' sweet gold");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }
    }
}
