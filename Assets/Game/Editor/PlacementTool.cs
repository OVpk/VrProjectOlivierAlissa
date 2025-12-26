using UnityEngine;
using UnityEditor;

public class PlacementTool : EditorWindow
{
    private GameObject prefab;
    private int rows = 5;
    private int columns = 5;
    private float spacing = 2f;
    private Vector3 startPosition = Vector3.zero;
    private Transform parent;

    [MenuItem("Tools/Placement Tool")]
    public static void ShowWindow()
    {
        GetWindow<PlacementTool>("Placement Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("Outil de Placement en Grille", EditorStyles.boldLabel);


        EditorGUILayout.Space();

        prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false) as GameObject;
        parent = EditorGUILayout.ObjectField("Parent", parent, typeof(Transform), true) as Transform;

        EditorGUILayout.Space();

        rows = EditorGUILayout.IntSlider("Lignes", rows, 1, 20);
        columns = EditorGUILayout.IntSlider("Colonnes", columns, 1, 20);
        spacing = EditorGUILayout.FloatField("Espacement", spacing);
        startPosition = EditorGUILayout.Vector3Field("Position de départ", startPosition);

        EditorGUILayout.Space();

        GUI.enabled = prefab != null;
        if (GUILayout.Button("Générer Grille", GUILayout.Height(30)))
        {
            GenerateGrid();
        }
        GUI.enabled = true;

        if (GUILayout.Button("Nettoyer", GUILayout.Height(30)))
        {
            ClearGrid();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox($"Total d'objets : {rows * columns}", MessageType.Info);
    }

    void GenerateGrid()
    {
        if (prefab == null)
        {
            EditorUtility.DisplayDialog("Erreur", "Veuillez sélectionner un prefab", "OK");
            return;
        }

        Undo.IncrementCurrentGroup();
        Undo.SetCurrentGroupName("Générer Grille");
        int undoGroup = Undo.GetCurrentGroup();

        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                Vector3 position = startPosition + new Vector3(x * spacing, 0, z * spacing);
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instance.transform.position = position;

                if (parent != null)
                {
                    instance.transform.SetParent(parent);
                }

                Undo.RegisterCreatedObjectUndo(instance, "Créer objet");
            }
        }

        Undo.CollapseUndoOperations(undoGroup);
    }

    void ClearGrid()
    {
        if (parent != null)
        {
            if (EditorUtility.DisplayDialog("Confirmation",
                "Voulez-vous supprimer tous les enfants ?", "Oui", "Non"))
            {
                while (parent.childCount > 0)
                {
                    DestroyImmediate(parent.GetChild(0).gameObject);
                }
            }
        }
    }
}