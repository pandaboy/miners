using System;
using UnityEngine;
using System.Collections;
using FSM;
using Mapping;

namespace FSM.States
{
    // In this state, the miner goes to the saloon to drink
    public class QuenchThirst : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            if (miner.DestinationTile != TileType.Pub)
            {
                Debug.Log(miner.ID + "Boy, ah sure is thusty! Walking to the saloon");
                miner.DestinationTile = TileType.Pub;
            }
        }

        public override void Execute(Miner miner)
        {
            // Buying whiskey costs 2 gold but quenches thirst altogether
            miner.HowThirsty = 0;
            miner.MoneyInBank -= 2;
            Debug.Log(miner.ID + "That's mighty fine sippin' liquer");
            miner.StateMachine.ChangeState(new EnterMineAndDigForNugget());
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + "Leaving the saloon, feelin' good");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }
    }
}
