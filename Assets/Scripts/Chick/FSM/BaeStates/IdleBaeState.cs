using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBaeState : State
{
    private const float MINIMUM_ACTION_TIME = 5f;
    private const float MAXIMUM_ACTION_TIME = 20f;
    private float timer;

    internal override void Enter()
    {
        // reset turn head timer
        ResetTimer();
    }

    internal override void Update()
    {
        // decrease the timer
        timer -= Time.deltaTime;

        // if timer hits 0, turn head
        if (timer <= 0)
        {
            // jump!
            ((BaeChickFSM)fsm).myChick.Jump();

            // reset the timer to start counting again
            ResetTimer();
        }
    }

    internal override void Exit()
    {
        // no exit behavior
    }

    /// <summary>
    /// Call to give the timer a new random value.
    /// </summary>
    private void ResetTimer()
    {
        timer = Random.Range(MINIMUM_ACTION_TIME, MAXIMUM_ACTION_TIME);
    }
}
