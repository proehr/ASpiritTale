using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

public class Progress : MonoBehaviour
{
    public int savingInterval = 3;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Progress");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    public Data _data;
    // Start is called before the first frame update
    void Start()
    {
        if (!loadData())
        {
            ResetData();
        }
        ContinuesSaving();
    }
    public void ResetData()
    {
        _data.Highscore = 0;
        _data.ProgressNr = 0;
        saveData();
    }
    public void saveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Data.dat");
        bf.Serialize(file, this._data);
        file.Close();
        
        
        //Debug.Log("Char is saved with the values:");
        foreach (var thisVar in this._data.GetType().GetProperties())
        {
            //Debug.Log(thisVar.Name + " = " + thisVar.GetValue(this._CharData, null));
        }
    }
    public bool loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Data.dat", FileMode.Open);
            this._data = (Data) bf.Deserialize(file);
            file.Close();

            //Debug.Log("Char is loaded with the values:");
            foreach (var thisVar in this._data.GetType().GetProperties())
            {
                //Debug.Log(thisVar.Name + " = " + thisVar.GetValue(this._CharData, null));
            }

            return true;
        }
        return false;
    }

    void ContinuesSaving()
    {
        InvokeRepeating("saveData", savingInterval, savingInterval);
    }
}
[Serializable]
public class Data
{
    [SerializeField]
    private int progressNr;
    [SerializeField]
    private int highscore;

    public int ProgressNr
    {
        get => progressNr;
        set => progressNr = value;
    }

    public int Highscore
    {
        get => highscore;
        set => highscore = value;
    }
}