using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MemoryBoardSaver
{
    public static void SaveEnemyTrackerHistory(MemoryBoardUIController memoryBoardUIController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemyTracker.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        MemoryBoardData data = new MemoryBoardData(memoryBoardUIController);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MemoryBoardData LoadEnemyTrackerHistory()
    {
        string path = Application.persistentDataPath + "/enemyTracker.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            MemoryBoardData data = formatter.Deserialize(stream) as MemoryBoardData;
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
    public static void DeleteEnemyTrackerHistory()
    {
        string path = Application.persistentDataPath + "/enemyTracker.dat";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }
    
    public static bool CheckIfEnemyTrackerHistoryExists()
    {
        string path = Application.persistentDataPath + "/enemyTracker.dat";
        if(File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    
}
