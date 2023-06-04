using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomChick : BaseChick
{
    // path variables
    internal List<Tile> path { get; private set; }
    private Tile currentTile;

    // the fsm of this chick
    private MomChickFSM fsm;

    internal override void Initialize()
    {
        base.Initialize();

        // initially chick doesn't have a path
        path = null;
        currentTile = null;

        // setup the state machine
        fsm = new MomChickFSM();
        fsm.Initialize(this);
    }

    protected override void Update()
    {
        // update the state machine
        fsm.Update();
    }

    /// <summary>
    /// Sets the path and starts following it.
    /// </summary>
    /// <param name="path">The path to follow</param>
    internal void FollowPath(List<Tile> path)
    {
        // remember the path
        this.path = path;

        // goto the follow path state
        fsm.GotoState(fsm.followPathState);
    }

    /// <summary>
    /// Resets the path.
    /// </summary>
    internal void ResetPath()
    {
        // reset the path to null
        path = null;
        currentTile = null;

        // stop moving
        Move(Vector3.zero);

        // go back to the idle state
        fsm.GotoState(fsm.idleState);
    }

    internal override void Move(Vector3 direction)
    {
        // determine the current tile
        currentTile = GameManager.instance.tileMap.GetTileAtWithRaycast(transform);

        // determine whether to move or run based on
        // the tile the chick is currently on and its cost
        running = currentTile == null ? false : currentTile.cost <= 10;

        base.Move(direction);
    }

    /// <summary>
    /// Call to trigger the turn head animation.
    /// </summary>
    internal void TurnHead()
    {
        animator.SetTrigger("TurnHead");
    }
}
