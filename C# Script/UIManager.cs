using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField descInput;
    public TMP_InputField idInput;
    public TMP_Text outputText;

    private ARDatabase db;

    void Start()
    {
        db = FindObjectOfType<ARDatabase>();
        Debug.Log("UIManager initialized");
    }

    public void OnCreate()
    {
        db.CreateExhibit(nameInput.text, descInput.text);
        outputText.text = "Exhibit created.";
        Debug.Log("Exhibit created.");
    }

    public void OnReadAll()
    {
        List<Exhibit> exhibits = db.GetAllExhibits();
        outputText.text = string.Format("{0,-5} | {1,-12} | {2}\n", "ID", "Name", "Description");
        outputText.text += new string('-', 48) + "\n";

        foreach (var ex in exhibits)
        {
            outputText.text += string.Format("{0,-5} | {1,-12} | {2}\n", ex.ID, ex.Name, ex.Description);
        }

        Debug.Log("Exhibits listed: " + exhibits.Count);
    }

    public void OnUpdate()
    {
        int id;
        if (int.TryParse(idInput.text, out id))
        {
            db.UpdateExhibit(id, nameInput.text, descInput.text);
            outputText.text = $"Exhibit {id} updated.";
            Debug.Log("Exhibit updated.");
        }
        else
        {
            outputText.text = "Invalid ID input.";
            Debug.Log("Update failed: Invalid ID input.");
        }
    }

    public void OnDelete()
    {
        int id;
        if (int.TryParse(idInput.text, out id))
        {
            db.DeleteExhibit(id);
            outputText.text = $"Exhibit {id} deleted.";
            Debug.Log("Exhibit deleted.");
        }
        else
        {
            outputText.text = "Invalid ID input.";
            Debug.Log("Delete failed: Invalid ID input.");
        }
    }
}
