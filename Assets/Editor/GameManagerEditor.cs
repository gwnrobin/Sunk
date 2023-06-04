using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    // define the property
    public SerializedProperty
        loading_Prop,
        lvlExpressionFileName_Prop,
        lvlBaseBitmap_Prop,
        lvlDecorationsBitmap_Prop;

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void OnEnable()
    {
        // setup the properties
        loading_Prop = serializedObject.FindProperty("loadingType");
        lvlExpressionFileName_Prop = serializedObject.FindProperty("lvlExpressionFileName");
        lvlBaseBitmap_Prop = serializedObject.FindProperty("lvlBaseBitmap");
        lvlDecorationsBitmap_Prop = serializedObject.FindProperty("lvlDecorationsBitmap");
    }

    public override void OnInspectorGUI()
    {
        // call base
        base.OnInspectorGUI();

        // update the GM
        serializedObject.Update();

        // always show the loading type property
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Level Loading Settings");
        EditorGUILayout.PropertyField(loading_Prop);

        // what is the current loading type?
        GameManager.LoadingType loadingType = (GameManager.LoadingType)loading_Prop.enumValueIndex;

        // switch on the loading type
        switch (loadingType)
        {
            case GameManager.LoadingType.Bitmap:
                // and show either the expression property
                EditorGUILayout.PropertyField(lvlBaseBitmap_Prop);
                EditorGUILayout.PropertyField(lvlDecorationsBitmap_Prop);
                break;

            case GameManager.LoadingType.Expression:
                // or the bit map properties
                EditorGUILayout.PropertyField(lvlExpressionFileName_Prop);
                break;
        }

        // apply the changes to the object
        serializedObject.ApplyModifiedProperties();
    }
}
