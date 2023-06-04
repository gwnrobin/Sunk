using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathMomState : State
{
    private List<Tile> pathRef;
    private int currentTileIndex;

    private MomChick MyChick { get { return ((MomChickFSM)fsm).myChick; } }

    internal override void Enter()
    {
        // start at the beginning of the path
        currentTileIndex = 0;

        // keep a reference to the path
        pathRef = ((MomChickFSM)fsm).myChick.path;
    }

    internal override void Update()
    {
        // as long as there is a next tile to go to... 
        if (currentTileIndex < pathRef.Count)
        {
            // check to see if the tile is reached, which is true if the chick
            // will reach that tile in this frame
            if (DistToCurrentTile() < MyChick.CurrentSpeed * Time.deltaTime)
            {
                // if this is the last tile, make the chick walk the last part
                if (currentTileIndex == pathRef.Count - 1)
                    MyChick.Move(DirectionToCurrentTile());
                
                // go to next tile 
                GotoNextTile();
                
                // stop this update
                return;
            }

            // make chick walk towards current tile
            MyChick.Move(DirectionToCurrentTile());
        }
    }

    internal override void Exit()
    {
        // stop moving
        MyChick.Move(Vector3.zero);
    }

    private float DistToCurrentTile()
    {
        // get the positions
        Vector3 tilePos = pathRef[currentTileIndex].transform.position;
        Vector3 chickPos = MyChick.transform.position;

        // ignore y axis
        tilePos.y = chickPos.y = 0;

        // return the length of the vector from tile to chick
        return (tilePos - chickPos).magnitude;
    }

    private Vector3 DirectionToCurrentTile()
    {
        // get the positions
        Vector3 tilePos = pathRef[currentTileIndex].transform.position;
        Vector3 chickPos = MyChick.transform.position;

        // ignore y axis
        tilePos.y = chickPos.y = 0;

        // return the normalized vector from chick to tile
        // to get the direction
        return (tilePos - chickPos).normalized;
    }

    private void GotoNextTile()
    {
        // increase the tile index
        currentTileIndex++;

        // if we reached the end of the path
        // we're done following the path, goto eat seeds state
        if(currentTileIndex >= pathRef.Count)
        {
            fsm.GotoState(((MomChickFSM)fsm).eatSeedsState);
            return;
        }

        // set either run or walk
        // based on the tile type (cost)
        if (pathRef[currentTileIndex])
        {

        }
    }
}
