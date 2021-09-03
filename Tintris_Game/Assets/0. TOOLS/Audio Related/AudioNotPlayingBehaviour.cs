using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioNotPlayingBehaviour : MonoBehaviour
{
    private AudioSource _myAudioSource;
    
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayIfNotPlaying()
    {
        if (!_myAudioSource.isPlaying)
        {
            _myAudioSource.Play();
        }
    }
}
