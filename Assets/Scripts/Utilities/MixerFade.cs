using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public static class MixerFade
{
  // Audio fade
  public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
  {
    float currentTime = 0;
    float currentVolume;
    audioMixer.GetFloat(exposedParam, out currentVolume);
    currentVolume = Mathf.Pow(10, currentVolume / 20);
    float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

    while (currentTime < duration)
    {
      currentTime += Time.deltaTime;
      float newVolume = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
      audioMixer.SetFloat(exposedParam, Mathf.Log10(newVolume) * 20);
      yield return null;
    }
    yield break;
  }
}
