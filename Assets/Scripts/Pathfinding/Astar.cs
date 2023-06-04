using UnityEngine;

[CreateAssetMenu(menuName = "Search Algorithms/A*")]
public class Astar : SearchAlgorithm
{
    protected override void UpdateTileCosts(Tile current, Tile next)
    {
        // a* bases the cost on both G and H cost,
        // the heuristic cost to the end 
        // and the distance since start
        next.gCost = current.gCost + CalculateCostToEnterTile(current, next);
        next.hCost = CalculateHCost(next);
    }

    private float CalculateHCost(Tile tile)
    {
        // calculate the minimal distance walking horizontally / vertically and diagonally
        float distanceX = Mathf.Abs(tile.transform.position.x - end.transform.position.x);
        float distanceY = Mathf.Abs(tile.transform.position.z - end.transform.position.z);
        float distance;

        if (distanceX >= distanceY)
        {
            distance = (distanceX - distanceY) + distanceY * 1.4f;
        }
        else
        {
            distance = (distanceY - distanceX) + distanceX * 1.4f;
        }

        // return the heuristic
        return distance * 10;
    }
}
