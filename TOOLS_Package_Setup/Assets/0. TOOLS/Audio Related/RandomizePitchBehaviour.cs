using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomizePitchBehaviour : MonoBehaviour
{
    public float minPitch, maxPitch;
    
    private AudioSource _myAudioSource;
    
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
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
