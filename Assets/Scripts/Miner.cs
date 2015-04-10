using UnityEngine;
using System.Collections;
using FSM;
using States;

public class Miner : Agent {

    public int MaxNuggets = 3;
    public int ThirstLevel = 5;
    public int ComfortLevel = 5;
    public int TirednessThreshold = 5;

    private StateMachine<Miner> stateMachine;
    public StateMachine<Miner> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    private int wifeId;
    public int WifeID
    {
        get { return wifeId; }
        set { wifeId = value; }
    }

    private Location location;
    public Location MinerLocation
    {
        get { return location; }
        set { location = value; }
    }

    private int goldCarrying;
    public int GoldCarrying
    {
        get { return goldCarrying; }
        set { goldCarrying = value; }
    }

    private int moneyInBank;
    public int MoneyInBank
    {
        get { return moneyInBank; }
        set { moneyInBank = value; }
    }

    private int howThirsty;
    public int HowThirsty
    {
        get { return howThirsty; }
        set { howThirsty = value; }
    }

    private int howFatigued;
    public int HowFatigued
    {
        get { return howFatigued; }
        set { howFatigued = value; }
    }

    public Miner() : base()
    {
        stateMachine = new StateMachine<Miner>(this);
        stateMachine.CurrentState = new GoHomeAndSleepTillRested();
        stateMachine.GlobalState = new MinerGlobalState();
        wifeId = this.ID + 1;
    }

    public void Awake()
    {
        //this.ID = 1;
        //wifeId = 2;
    }

    public override void Update()
    {
        howThirsty += 1;
        stateMachine.Update();
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return stateMachine.HandleMessage(telegram);
    }

    public bool PocketsFull()
    {
        if (goldCarrying >= MaxNuggets)
            return true;
        else
            return false;
    }

    public bool Thirsty()
    {
        if (howThirsty >= ThirstLevel)
            return true;
        else
            return false;
    }

    public bool Fatigued()
    {
        if (howFatigued >= TirednessThreshold)
            return true;
        else
            return false;
    }

    public bool Rich()
    {
        if (moneyInBank >= ComfortLevel)
            return true;
        else
            return false;
    }
}
