using UnityEngine;

[CreateAssetMenu(menuName = "Search Algorithms/Dijkstra")]
public class Dijkstra : SearchAlgorithm
{
    protected override void UpdateTileCosts(Tile current, Tile next)
    {
        // dijkstra bases the cost completely on the G cost,
        // the distance since start
        next.gCost = current.gCost + CalculateCostToEnterTile(current, next);
        next.hCost = 0;
    }
}