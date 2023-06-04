using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomGrammar.GrammarCore;

public class GameManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] internal TileMap tileMap;
    [SerializeField] private DebugInfoUI debugInfoUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private CameraController cameraController;
    [Space(20)]

    // the possible algorithms to use in pathfinding
    [SerializeField] internal List<SearchAlgorithm> searchAlgorithms;
    internal SearchAlgorithm currentSearchAlgorithm;

    [Header("Chick Settings")]
    [SerializeField] private GameObject momChickPrefab;
    [SerializeField] private GameObject baeChickPrefab;
    [SerializeField] private int baeChicksToSpawn = 3;
    private MomChick momChick;
    private List<BaeChick> baeChicks;
    private List<Tile> baeSpawnPoints;

    [Header("Debug Pathfinding Settings")]
    // colors for debug visuals
    [SerializeField] internal Color pathColor;
    [SerializeField] internal Color openColor;
    [SerializeField] internal Color closedColor;

    // visualization pathfinding variables
    [SerializeField] internal bool visualizeSearch;
    [SerializeField] [Range(1, 100)] internal float visualizationSpeed;

    [HideInInspector] public LoadingType loadingType = LoadingType.Bitmap;
    [HideInInspector] public string lvlExpressionFileName;
    [HideInInspector] public Texture2D lvlBaseBitmap;
    [HideInInspector] public Texture2D lvlDecorationsBitmap;
    public enum LoadingType { Bitmap, Expression }

    // the results from finding a path
    private SearchResult searchResult;
    internal bool IsSearching { get { return false; } }
    internal bool HasPath { get { return searchResult != null && searchResult.path != null; } }

    // semi singleton
    internal static GameManager instance;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // setup singleton
        instance = this;

        // initialize the UI
        debugInfoUI.Initialize();
        settingsUI.Initialize();

        // load the level
        LoadLevel();
    }

    /// <summary>
    /// Call to load the level set in the inspector.
    /// </summary>
    private void LoadLevel()
    {
        bool loadSuccesful = false;

        switch (loadingType)
        {
            case LoadingType.Bitmap:
                // cannot load the level if it's null
                if (lvlBaseBitmap == null || lvlDecorationsBitmap == null)
                    return;

                // load the level
                loadSuccesful = tileMap.CreateTileMap(lvlBaseBitmap, lvlDecorationsBitmap);
                break;

            case LoadingType.Expression:
                // cannot load the level if it's empty
                if (lvlExpressionFileName == null || lvlExpressionFileName == string.Empty)
                    return;

                // load the level
                loadSuccesful = tileMap.CreateTileMap(lvlExpressionFileName);
                break;
        }

        if (loadSuccesful)
        {
            // center camera on map
            cameraController.CenterOnTileMap();

            // spawn the chick at start
            momChick = Instantiate(momChickPrefab).GetComponent<MomChick>();
            momChick.transform.position = tileMap.startTile.transform.position;
            momChick.Initialize();

            // spawn its bae's
            baeChicks = new List<BaeChick>();
            baeSpawnPoints = tileMap.GetRandomFreeNeighbours(tileMap.startTile, baeChicksToSpawn);

            for (int i = 0; i < baeChicksToSpawn; i++)
            {
                // spawn a new chick and add it to the collection
                baeChicks.Add(Instantiate(baeChickPrefab).GetComponent<BaeChick>());

                // position the bae chick on its tile with a bit of rndm offset
                Vector2 rndmOffset = Random.insideUnitCircle * 0.3f;
                baeChicks[i].transform.localPosition = baeSpawnPoints[i].transform.position +
                    new Vector3(rndmOffset.x, 0, rndmOffset.y);

                // give the chick a random y rotation
                baeChicks[i].transform.localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));

                // initialize the chick
                baeChicks[i].Initialize();
            }
        }
    }

    /// <summary>
    /// Call to set the current search algorithm.
    /// </summary>
    /// <param name="newSearchAlgorithm">The new search algorithm</param>
    internal void SetCurrentSearchAlgorithm(SearchAlgorithm newSearchAlgorithm)
    {
        currentSearchAlgorithm = newSearchAlgorithm;
    }

    /// <summary>
    /// Called from UI to find a path if it exists with the current algorithm.
    /// </summary>
    internal void FindPath()
    {
        // make sure path is reset
        ResetPath();

        // if we need to visualize the search, 
        // it's executed over time with intervals
        // else, find path instant
        if (visualizeSearch)
            StartCoroutine(currentSearchAlgorithm.FindPathOverTime(tileMap.startTile, tileMap.endTile, OnFinishPath));
        else
            currentSearchAlgorithm.FindPath(tileMap.startTile, tileMap.endTile, OnFinishPath);
    }

    /// <summary>
    /// Called when the search algorithm has finished.
    /// </summary>
    /// <param name="searchResult"></param>
    internal void OnFinishPath(SearchResult searchResult)
    {
        // save the result
        this.searchResult = searchResult;

        // throw error and return if no path was found
        if(searchResult.path == null || searchResult.path.Count == 0)
        {
            Debug.LogError("No valid path found!");
            return;
        }

        // draw path if we show debug visuals
        if (visualizeSearch)
            DrawCurrentPath();

        // set debug info
        debugInfoUI.SetText(searchResult);

        // update interactive state of buttons in setting ui
        settingsUI.SetInteractableStateButtons();
    }

    /// <summary>
    /// Draws the current path with a line renderer.
    /// </summary>
    private void DrawCurrentPath()
    {
        // cant draw the path if it doesn't exist!
        if (searchResult.path == null || searchResult.path.Count <= 0)
            return;

        // set debug visuals for the tiles in the path
        for (int i = 0; i < searchResult.path.Count; i++)
            searchResult.path[i].SetDebugVisual(true, pathColor);
    }

    /// <summary>
    /// Called from UI to make the chick start following the path.
    /// </summary>
    internal void FollowPath()
    {
        // give the path to chick and make him follow it
        momChick.FollowPath(searchResult.path);
    }

    /// <summary>
    /// Called from UI to reset chick and path.
    /// </summary>
    internal void ResetPath()
    {
        // reset the search results
        searchResult = null;

        // dont show debug visuals anymore
        tileMap.ClearTilesDebugVisual();

        // reset the chick
        momChick.transform.position = tileMap.startTile.transform.position;
        momChick.ResetPath();

        // reset debug info
        debugInfoUI.SetText(null);

        // update interactive state of buttons in setting ui
        settingsUI.SetInteractableStateButtons();
    }
}
