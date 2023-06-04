using UnityEngine;

[CreateAssetMenu(menuName = "Search Algorithms/BFS")]
public class BFS : SearchAlgorithm
{
    protected override void UpdateTileCosts(Tile current, Tile next)
    {
        // bfs bases the cost completely on the H cost,
        // the heuristic cost to the end
        next.gCost = 0;
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
