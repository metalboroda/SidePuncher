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

    public float RandomPitch()
    {
      return _audioSource.pitch = Random.Range(0.95f, 1.05f);
    }
  }
}