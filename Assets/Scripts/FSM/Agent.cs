using System;
using UnityEngine;
using System.Collections;
using Mapping;

namespace FSM
{
    abstract public class Agent : MonoBehaviour
    {
        private static int nAgents = 0;

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private TileType currentTile;
        public TileType CurrentTile
        {
            get { return currentTile; }
            set { currentTile = value; }
        }

        private TileType nextTile;
        public TileType NextTile
        {
            get { return nextTile; }
            set { nextTile = value; }
        }

        public Agent() : base()
        {
            id = nAgents++;
        }

        abstract public void Update();
        abstract public bool HandleMessage(Telegram telegram);
    }
}
