using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public enum TileType { None, Water, Dirt, Grass, Sand, }
public enum DecorationType { None, Plant, Bush, Tree, BigRock, SmallRock, }
public enum SpecialTileType { None, Start, End }

[Serializable]
public struct TileMaterialPair
{
    public TileType type;
    public Material material;
}

[Serializable]
public struct TileCostPair
{
    public TileType type;
    public int cost;
}

[Serializable]
public struct DecorationTypeObjectsPair
{
    public DecorationType type;
    public List<GameObject> objects;
}

[Serializable]
public struct TileColorPair
{
    public TileType type;
    public Color color;
}

[Serializable]
public struct DecorationColorPair
{
    public DecorationType type;
    public Color color;
}

[Serializable]
public struct SpecialTileColorPair
{
    public SpecialTileType type;
    public Color color;
}

public class Tile : MonoBehaviour
{
    // reference to meshrenderer 
    [SerializeField] private MeshRenderer meshRenderer;
    private Color originalColor;

    // the type of this tile
    TileType type;
    
    // references to all neighbouring tiles
    internal List<Tile> neighbours;

    // whether chick can enter this tile
    internal bool canEnter;

    // cost to enter this tile
    // -1 is infinite
    internal int cost;

    // variables used / changed during pathfinding
    internal float gCost;
    internal float hCost;
    internal float fCost { get { return gCost + hCost; } }
    internal Tile beforeInPath;

    // keep track of decorations added
    private List<GameObject> decorations;

    /// <summary>
    /// Call to set up this tile.
    /// </summary>
    /// <param name="type">The type of tile</param>
    internal void Initialize(TileType type)
    {
        // remember type
        this.type = type;

        // set material
        meshRenderer.material = GameManager.instance.tileMap.GetMaterialForTileType(type);
        originalColor = meshRenderer.material.color;

        // get cost
        cost = GameManager.instance.tileMap.GetCostForTileType(type);

        // can't enter if cost is -1
        canEnter = cost >= 0;

        // start with empty lists for decorations and neighbors
        decorations = new List<GameObject>();
        neighbours = new List<Tile>();
    }

    /// <summary>
    /// Adds and positions decoration on this tile.
    /// </summary>
    /// <param name="go">The new decoration as a GameObject</param>
    /// <param name="canWalkThrough">Whether the chick can walk through this decoration</param>
    internal void AddDecoration(GameObject go, bool canWalkThrough, bool randomize = true)
    {
        if (randomize)
        {
            // position the object on this tile with a bit of rndm offset
            Vector2 rndmOffset = UnityEngine.Random.insideUnitCircle * 0.3f;
            go.transform.localPosition = transform.localPosition + new Vector3(rndmOffset.x, 0, rndmOffset.y);

            // give the object a random y rotation
            go.transform.localRotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0));

            // give the object a small random change in scale
            float rndmScale = go.transform.localScale.x + UnityEngine.Random.Range(-0.2f, 0.2f);
            go.transform.localScale = new Vector3(rndmScale, rndmScale, rndmScale);
        }
        else
        {
            // position the object on this tile 
            go.transform.localPosition = transform.localPosition;

            // give the object a random y rotation
            go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        // add it to decorations to keep track of it
        decorations.Add(go);

        // cannot enter this tile if chick
        // cannot walk through this decoration
        if (!canWalkThrough)
            canEnter = false;
    }

    /// <summary>
    /// Resets tiles pathfinding variables. 
    /// Readies it for finding new path. 
    /// </summary>
    internal void ClearForPathfinding()
    {
        gCost = 0;
        hCost = 0;
        beforeInPath = null;
    }

    /// <summary>
    /// Call to control the debug visuals.
    /// Used to visualize the open and closed list in pathfinding.
    /// </summary>
    /// <param name="on">Whether the visual should be on or off</param>
    /// <param name="color">The color of the visual</param>
    internal void SetDebugVisual(bool on, Color color)
    {
        meshRenderer.material.SetColor("_SecColor", on ?  color : originalColor);
    }
}
