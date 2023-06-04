using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugInfoUI : MonoBehaviour
{
    // reference to text object
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject copyInstruction;

    private string debugText;

    /// <summary>
    /// Call to initialize this UI element.
    /// </summary>
    internal void Initialize()
    {
        // set the text to no results yet
        SetText(null);
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            GUIUtility.systemCopyBuffer = debugText;
    }

    /// <summary>
    /// Sets the debug text according to the search result.
    /// </summary>
    /// <param name="result">The search result</param>
    internal void SetText(SearchResult result)
    {
        // build the string
        string newText = "Debug Information:\n\n";
        debugText = string.Empty;

        // add information of the result if we have any
        if(result != null)
        {
            debugText = result.algorithmName + ";" + result.pathCost + ";" + result.tilesInPath + ";" +
                result.tilesEvaluated + ";" + result.time;

            newText += "Algorithm: " + result.algorithmName + "\n";
            newText += "Path cost: " + result.pathCost + "\n";
            newText += "Tiles in path: " + result.tilesInPath + "\n";
            newText += "Tiles evaluated: " + result.tilesEvaluated + "\n";
            newText += "Time: " + String.Format("{0:0.000}", result.time) + " sec\n";

            copyInstruction.gameObject.SetActive(true);
        }
        else
        {
            newText += "No results yet.";
            copyInstruction.gameObject.SetActive(false);
        }

        // set the text
        text.text = newText;
    }
}
