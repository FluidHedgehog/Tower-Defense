using UnityEngine;
using UnityEngine.Audio;

public class SettingsActions : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float f)
    {
        audioMixer.SetFloat("Music", f);
    }
}
