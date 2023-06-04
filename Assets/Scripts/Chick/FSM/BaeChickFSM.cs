using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaeChickFSM : FSM
{
    internal BaeChick myChick;

    internal IdleBaeState idleState;

    internal void Initialize(BaeChick myChick)
    {
        this.myChick = myChick;

        idleState = new IdleBaeState();
        idleState.Initialize(this);

        base.Initialize();
    }

    protected override State GetInitialState()
    {
        return idleState;
    }
}