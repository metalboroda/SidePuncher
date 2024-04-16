using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.SOs
{
  [CreateAssetMenu(fileName = "AudioContainer", menuName = "Audio/AudioContainer")]
  public class AudioContainerSO : ScriptableObject
  {
    public List<AudioClip> audioClips = new List<AudioClip>();

    public AudioClip GetRandomClip()
    {
      if (audioClips == null || audioClips.Count == 0) return null;

      int randomIndex = Random.Range(0, audioClips.Count);

      return audioClips[randomIndex];
    }
  }
}