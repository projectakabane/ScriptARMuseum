using UnityEngine;
using System.IO;
using System.Collections.Generic;
using SQLite4Unity3d;
using System.Linq;

public class Exhibit
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
}

public class ARDatabase : MonoBehaviour
{
    private SQLiteConnection db;
    private string dbFileName = "MuseumDB.sqlite";

    void Awake()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, dbFileName);

#if UNITY_ANDROID && !UNITY_EDITOR
        string streamingPath = Path.Combine(Application.streamingAssetsPath, dbFileName);
        string persistentPath = dbPath;

        if (!File.Exists(persistentPath))
        {
            var www = new WWW(streamingPath);
            while (!www.isDone) { }
            File.WriteAllBytes(persistentPath, www.bytes);
        }
#else
        string sourcePath = Path.Combine(Application.streamingAssetsPath, dbFileName);
        if (!File.Exists(dbPath))
        {
            File.Copy(sourcePath, dbPath);
        }
#endif

        db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        db.CreateTable<Exhibit>();
    }

    public void CreateExhibit(string name, string description)
    {
        Exhibit newExhibit = new Exhibit { Name = name, Description = description };
        db.Insert(newExhibit);
    }

    public List<Exhibit> GetAllExhibits()
    {
        return db.Table<Exhibit>().ToList();
    }

    public Exhibit GetExhibitById(int id)
    {
        return db.Find<Exhibit>(id);
    }

    public void UpdateExhibit(int id, string name, string description)
    {
        Exhibit exhibit = db.Find<Exhibit>(id);
        if (exhibit != null)
        {
            exhibit.Name = name;
            exhibit.Description = description;
            db.Update(exhibit);
        }
    }

    public void DeleteExhibit(int id)
    {
        db.Delete<Exhibit>(id);
    }

    public void ClearTableAndResetID()
    {
        db.DeleteAll<Exhibit>();
        // SQLite4Unity3d does not support deleting from sqlite_sequence directly
        // You must delete and recreate the DB file if you want to reset ID
        // Or run a raw query if supported:
        db.Execute("DELETE FROM sqlite_sequence WHERE name = 'Exhibit'");
    }
}
