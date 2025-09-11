using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private HashSet<string> collected = new HashSet<string>();

    const string Key = "INV_COLLECTED";

    [Header("Behavior")]
    public bool resetOnPlay = true;            // reset setiap Play
    public bool persistBetweenPlays = false;   // jika true, simpan/load dari PlayerPrefs

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (resetOnPlay)
        {
            collected.Clear();
            PlayerPrefs.DeleteKey(Key);
        }
        else if (persistBetweenPlays)
        {
            Load();
        }
        else
        {
            collected.Clear();
        }
    }

    public bool Has(string id) => collected.Contains(id);

    public void Add(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        if (collected.Add(id) && persistBetweenPlays) Save();
    }

    void Save()
    {
        var data = string.Join("|", collected);
        PlayerPrefs.SetString(Key, data);
        PlayerPrefs.Save();
    }

    void Load()
    {
        collected.Clear();
        var data = PlayerPrefs.GetString(Key, "");
        if (!string.IsNullOrEmpty(data))
            foreach (var s in data.Split('|'))
                if (!string.IsNullOrEmpty(s)) collected.Add(s);
    }
}
