using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{
    public class EnterMineAndDigForNugget : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.Id + "Walkin' to the goldmine");
            miner.MinerLocation = Location.mine;
        }

        public override void Execute(Miner miner)
        {
            miner.GoldCarrying += 1;
            miner.HowFatigued += 1;
            Debug.Log(miner.Id + "Pickin' up a nugget");
            
            if (miner.PocketsFull()) {
                miner.StateMachine.ChangeState(new VisitBankAndDepositGold());
            }

            if (miner.Thirsty()) {
                miner.StateMachine.ChangeState(new QuenchThirst());
            }
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.Id + "Ah'm leaving the gold mine with mah pockets full o' sweet gold");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }
    }
}
