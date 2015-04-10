﻿using System;
using UnityEngine;
using System.Collections;
using FSM;
using States;

public class Wife : Agent
{
    private StateMachine<Wife> stateMachine;
    public StateMachine<Wife> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    private int husbandId;
    public int HusbandID
    {
        get { return husbandId; }
        set { husbandId = value; }
    }

    private Location location;
    public Location MinerLocation
    {
        get { return location; }
        set { location = value; }
    }

    private Boolean cooking;
    public Boolean Cooking
    {
        get { return cooking; }
        set { cooking = value; }
    }

    public Wife() : base()
    {
        stateMachine = new StateMachine<Wife>(this);
        stateMachine.CurrentState = new DoHouseWork();
        stateMachine.GlobalState = new WifesGlobalState();
        husbandId = this.ID - 1;
    }

    public void Awake()
    {
        //this.ID = 2;
        //husbandId = 1;
    }

    public override void Update()
    {
        stateMachine.Update();
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return stateMachine.HandleMessage(telegram);
    }
}
