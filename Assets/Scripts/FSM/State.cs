using System;
using UnityEngine;
using System.Collections;

namespace FSM
{
    abstract public class State <T>
    {
        abstract public void Enter(T agent);

        abstract public void Exit(T agent);

        abstract public void Execute(T agent);

        abstract public bool OnMessage(T agent, Telegram telegram);
    }
}
