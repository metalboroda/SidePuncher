using UnityEngine;

namespace Assets.__Game.Scripts.Components
{
  public class AudioComponent
  {
    private AudioSource _audioSource;

    public AudioComponent(AudioSource audioSource)
    {
      _audioSource = audioSource;
    }

    public float RandomPitch(float min = 0.9f, float max = 1.1f)
    {
      return _audioSource.pitch = Random.Range(min, max);
    }
  }
}