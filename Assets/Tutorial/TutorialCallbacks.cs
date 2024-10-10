using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;

/// <summary>
/// Implement your Tutorial callbacks here.
/// </summary>
[CreateAssetMenu(fileName = DefaultFileName, menuName = "Tutorials/" + DefaultFileName + " Instance")]
public class TutorialCallbacks : ScriptableObject
{
    public GameObject TokenToSelect;
    /// <summary>
    /// The default file name used to create asset of this class type.
    /// </summary>
    public const string DefaultFileName = "TutorialCallbacks";

    /// <summary>
    /// Creates a TutorialCallbacks asset and shows it in the Project window.
    /// </summary>
    /// <param name="assetPath">
    /// A relative path to the project's root. If not provided, the Project window's currently active folder path is used.
    /// </param>
    /// <returns>The created asset</returns>
    public static ScriptableObject CreateAndShowAsset(string assetPath = null)
    {
        assetPath = assetPath ?? $"{TutorialEditorUtils.GetActiveFolderPath()}/{DefaultFileName}.asset";
        var asset = CreateInstance<TutorialCallbacks>();
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
        EditorUtility.FocusProjectWindow(); // needed in order to make the selection of newly created asset to really work
        Selection.activeObject = asset;
        return asset;
    }

    /// <summary>
    /// Example callback for basic UnityEvent
    /// </summary>
    public void ExampleMethod()
    {
        Debug.Log("ExampleMethod");
    }

    /// <summary>
    /// Example callbacks for ArbitraryCriterion's BoolCallback
    /// </summary>
    /// <returns></returns>
    public bool DoesFooExist()
    {
        return GameObject.Find("Foo") != null;
    }

    /// <summary>
    /// Implement the logic to automatically complete the criterion here, if wanted/needed.
    /// </summary>
    /// <returns>True if the auto-completion logic succeeded.</returns>
    public bool AutoComplete()
    {
        var foo = GameObject.Find("Foo");
        if (!foo)
            foo = new GameObject("Foo");
        return foo != null;
    }

    public void SelectSpawnedGameObject(FutureObjectReference futureObjectReference)
    {
        if (futureObjectReference.SceneObjectReference==null) { return; }
        SelectGameObject(futureObjectReference.SceneObjectReference.ReferencedObjectAsGameObject);
    }

    public void SelectGameObject(GameObject gameObject)
    {
        if (!gameObject) { return; }
        Selection.activeObject=gameObject;
    }

    public void SelectToken()
    {
        if (!TokenToSelect)
        {
            TokenToSelect=GameObject.FindGameObjectWithTag("TutorialRequirement");
            if (!TokenToSelect)
            {
                Debug.LogErrorFormat("A TokenInstance with the tag '{0}' must be in the scene in order to make this tutorial work properly. Please add the tag {0} to one of your tokens in the scene", "TutorialRequirement");
                return;
            }
        }
        SelectGameObject(TokenToSelect);
    }

    public void SelectMoveTool()
    {
        Tools.current=Tool.Move;
    }

    public void SelectRotateTool()
    {
        Tools.current=Tool.Rotate;
    }

    public void StartTutorial(Tutorial tutorial)
    {
        TutorialWindowUtils.StartTutorial(tutorial);
    }
}
