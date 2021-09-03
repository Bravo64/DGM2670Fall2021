using System.Collections;
using GameEvents;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public IntData badDotCountObj;
    public VoidEvent updateBadDotTextEvent;
    public SceneLoader sceneLoader;
    public RandomizePitchBehaviour audioPitchRandomizer;
    public RandomizePitchBehaviour smashAudioPitchRandomizer;
    public IntData explosionAlreadyPlaying;
    public ParticleSystem myParticles;
    public ParticleSystem mySmashParticles;

    private bool _destructionPeriod = true;

    private void Awake()
    {
        if (explosionAlreadyPlaying.value == 0)
        {
            audioPitchRandomizer.RandomizePitchAndPlay();
            smashAudioPitchRandomizer.RandomizePitchAndPlay();
            explosionAlreadyPlaying.value = 1;
            myParticles.Play();
            mySmashParticles.Play();
        }
        else
        {
            var emission = myParticles.emission;
            emission.burstCount = Mathf.RoundToInt(emission.burstCount / 5.0f);
            myParticles.Play();
        }

        StartCoroutine(WaitToDisable());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_destructionPeriod)
        {
            if (other.gameObject.name == "Bad Dot" || other.gameObject.name == "Frozen Dot")
            {
                other.gameObject.name = "Dot";
                badDotCountObj.value--;
                updateBadDotTextEvent.Raise();
                if (badDotCountObj.value <= 0)
                {
                    sceneLoader.LoadNextScene();
                }
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
            {
                StartCoroutine(WaitToCall(other));
                return;
            }

            if (other.name != "Color Sprite")
            {
                other.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator WaitToCall(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        if (other.gameObject.activeSelf)
        {
            other.GetComponent<InstantiationBehavior>().ActivateInstantiation();
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitToDisable()
    {
        yield return 2;
        _destructionPeriod = false;
    }
}
