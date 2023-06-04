using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaeChick : BaseChick
{
    // the fsm of this chick
    private BaeChickFSM fsm;

    internal override void Initialize()
    {
        base.Initialize();

        // setup the state machine
        fsm = new BaeChickFSM();
        fsm.Initialize(this);
    }

    protected override void Update()
    {
        // update the state machine
        fsm.Update();
    }

    /// <summary>
    /// Call to trigger the jump animation.
    /// </summary>
    internal void Jump()
    {
        animator.SetTrigger("Jump");
    }
}
