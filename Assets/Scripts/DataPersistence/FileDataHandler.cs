using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId)
    {
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading data from: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data, string profileId)
    {
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            File.WriteAllText(fullPath, dataToStore);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving data to: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Data not found for profile: " + profileId);
                continue;
            }

            GameData profileData = Load(profileId);
            
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else{
                Debug.Log("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }

        return profileDictionary;
    }
}
