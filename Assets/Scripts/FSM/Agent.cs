using System;
using UnityEngine;
using System.Collections;

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

        public Agent() : base()
        {
            id = nAgents++;
        }

        abstract public void Update();
        abstract public bool HandleMessage(Telegram telegram);
    }
}
