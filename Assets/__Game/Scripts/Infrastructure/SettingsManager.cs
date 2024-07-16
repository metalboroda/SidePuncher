using System.IO;
using UnityEngine;

namespace Assets.__Game.Scripts.Infrastructure
{
  public class SettingsManager
  {
    private const string settingsFileName = "settings.json";

    public static T LoadSettings<T>() where T : new() {
      T settings = new T();

      string filePath = Application.persistentDataPath + "/" + settingsFileName;

      if (File.Exists(filePath)) {
        string json = File.ReadAllText(filePath);

        settings = JsonUtility.FromJson<T>(json);
      }

      return settings;
    }

    public static void SaveSettings<T>(T settings) {
      string json = JsonUtility.ToJson(settings);
      string filePath = Application.persistentDataPath + "/" + settingsFileName;

      File.WriteAllText(filePath, json);
    }
  }
}