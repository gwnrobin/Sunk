using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomChickFSM : FSM
{
    internal MomChick myChick;

    internal IdleMomState idleState;
    internal FollowPathMomState followPathState;
    internal EatSeedsMomState eatSeedsState;

    internal void Initialize(MomChick myChick) 
    {
        this.myChick = myChick;

        idleState = new IdleMomState();
        idleState.Initialize(this);

        followPathState = new FollowPathMomState();
        followPathState.Initialize(this);

        eatSeedsState = new EatSeedsMomState();
        eatSeedsState.Initialize(this);

        base.Initialize();
    }

    protected override State GetInitialState()
    {
        return idleState;
    }
}
