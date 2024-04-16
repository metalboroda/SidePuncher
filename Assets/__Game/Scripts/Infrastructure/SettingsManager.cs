using UnityEngine;

namespace Assets.__Game.Scripts.Infrastructure
{
  public class SettingsManager
  {
    private const string settingsFileName = "settings.json";

    public static T LoadSettings<T>()
    {
      T settings = default;

      string filePath = Application.persistentDataPath + "/" + settingsFileName;

      if (System.IO.File.Exists(filePath))
      {
        string json = System.IO.File.ReadAllText(filePath);

        settings = JsonUtility.FromJson<T>(json);
      }

      return settings;
    }

    public static void SaveSettings<T>(T settings)
    {
      string json = JsonUtility.ToJson(settings);
      string filePath = Application.persistentDataPath + "/" + settingsFileName;

      System.IO.File.WriteAllText(filePath, json);
    }
  }
}