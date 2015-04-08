using System;
using UnityEngine;
using System.Collections;
using FSM;

namespace States
{
    public class MinerGlobalState : State<Miner>
    {
        public override void Enter(Miner miner)
        {

        }

        public override void Execute(Miner miner)
        {

        }

        public override void Exit(Miner miner)
        {

        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }
    }
}
