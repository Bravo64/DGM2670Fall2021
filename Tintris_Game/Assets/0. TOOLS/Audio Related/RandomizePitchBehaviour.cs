using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class RandomizePitchBehaviour : MonoBehaviour
{
    public float minPitch, maxPitch;
    public bool playOnEnable = false;
    
    
    public AudioSource _myAudioSource;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            RandomizePitchAndPlay();
        }
    }

    public void RandomizePitchWithoutPlaying()
    {
        _myAudioSource.pitch = Random.Range(minPitch, maxPitch);
    }
    
    public void RandomizePitchAndPlay()
    {
        _myAudioSource.pitch = Random.Range(minPitch, maxPitch);
        _myAudioSource.Play();
    }
    
    public void PlayRandomIfNotPlaying()
    {
        if (!_myAudioSource.isPlaying)
        {
            _myAudioSource.pitch = Random.Range(minPitch, maxPitch);
            _myAudioSource.Play();
        }
    }
}
