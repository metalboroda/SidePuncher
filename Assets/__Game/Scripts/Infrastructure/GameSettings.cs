using System;

namespace Assets.__Game.Scripts.Infrastructure
{
  [Serializable]
  public class GameSettings
  {
    #region Database
    public string UserName;
    #endregion

    #region Game Settings
    public bool IsMusicOn = true;
    public bool IsSFXOn = true;
    #endregion
  }
}