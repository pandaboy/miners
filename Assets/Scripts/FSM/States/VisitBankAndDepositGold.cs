using System;
using UnityEngine;
using System.Collections;
using FSM;
using Mapping;

namespace FSM.States
{
    public class VisitBankAndDepositGold : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + "Goin' to the bank. Yes siree");
            miner.MinerLocation = Location.bank;
            miner.DestinationTile = TileType.Bank;
        }

        public override void Execute(Miner miner)
        {
            miner.MoneyInBank += miner.GoldCarrying;
            miner.GoldCarrying = 0;

            Debug.Log(miner.ID + "Depositing gold. Total savings now: " + miner.MoneyInBank);
            if (miner.Rich()) {
                Debug.Log(miner.ID + "WooHoo! Rich enough for now. Back home to mah li'lle lady");
                miner.StateMachine.ChangeState(new GoHomeAndSleepTillRested());
            }
            else {
                miner.StateMachine.ChangeState(new EnterMineAndDigForNugget());
            }
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + "Leavin' the Bank");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }
    }
}
