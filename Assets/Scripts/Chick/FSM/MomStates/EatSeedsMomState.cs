using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSeedsMomState : State
{
    internal override void Enter()
    {
        // start eating animation
        ((MomChickFSM)fsm).myChick.Eat(true);
    }

    internal override void Update()
    {
        // no update behavior
    }

    internal override void Exit()
    {
        // end eating animation
        ((MomChickFSM)fsm).myChick.Eat(false);
    }
}
