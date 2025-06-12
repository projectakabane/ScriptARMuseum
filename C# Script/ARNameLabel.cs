using UnityEngine;
using TMPro;
using Vuforia;

public class ExhibitDisplay : MonoBehaviour
{
    public int exhibitID = 1;
    public TextMeshPro nameText;

    private ARDatabase db;
    private ObserverBehaviour observer;

    void Awake()
    {
        db = FindFirstObjectByType<ARDatabase>();
        observer = GetComponent<ObserverBehaviour>();

        if (observer != null)
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnDestroy()
    {
        if (observer != null)
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            DisplayExhibitInfo();
        }
    }

    void DisplayExhibitInfo()
    {
        Debug.Log("Trying to display exhibit info...");

        if (db == null)
        {
            db = FindFirstObjectByType<ARDatabase>();
            Debug.Log("Database found: " + (db != null));
        }

        Exhibit ex = db.GetExhibitById(exhibitID);

        if (ex != null)
        {
            Debug.Log($"Loaded Exhibit: ID={ex.ID}, Name={ex.Name}");
            nameText.text = ex.Name;
        }
        else
        {
            Debug.LogWarning($"Exhibit with ID {exhibitID} not found.");
            nameText.text = "Not Found";
        }
    }

}
