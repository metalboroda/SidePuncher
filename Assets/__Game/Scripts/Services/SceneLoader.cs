using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.__Game.Scripts.Services
{
  public class SceneLoader
  {
    public void LoadSceneAsync(string sceneName, MonoBehaviour monoBehaviour, Action onComplete = null)
    {
      monoBehaviour.StartCoroutine(DoLoadSceneAsync(sceneName, onComplete));
    }

    private IEnumerator DoLoadSceneAsync(string sceneName, Action onComplete)
    {
      AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

      while (asyncOperation.isDone == false)
        yield return null;

      onComplete?.Invoke();
    }
  }
}