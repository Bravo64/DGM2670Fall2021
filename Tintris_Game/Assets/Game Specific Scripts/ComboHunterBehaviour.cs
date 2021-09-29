
using System;
using System.Collections.Generic;
using GameEvents;
using Unity.Mathematics;
using UnityEngine;

public class ComboHunterBehaviour : MonoBehaviour
{
    public VoidEvent activateGravityEvent;
    public IntData badDotCount;
    public VoidEvent updateBadDotText, levelCompleteEvent;
    public GameObject[] dotParticlePool;
    public GameObject[] badDotParticlePool;
    public GameObject bomb;
    public IntData currentComboCount;
    public AudioSource _awardAudioSource;
    public IntData turnsTaken;
    public IntData levelPersonalBest;
    public PlayerPrefLoader personalBestLoader;
    
    private bool _hunterEnabled;
    private string _targetTag;
    private List<Rigidbody2D> _currentComboTrain = new List<Rigidbody2D>();
    private List<DotBehaviour> _currentComboTrainScripts = new List<DotBehaviour>();

    private int _combosNeededToScore = 4;
    private int _currentFrame = 0;
    private int _baseIndex = 0;
    private int _maximumFrames = 2;
    private Rigidbody2D _dotParent;
    private int _currentIndex = 0;
    private bool _bombPlanted = false;
    private int _combosNeededForBomb = 3;

    public void ActivateComboHunter(GameObject inputGameObject)
    {
        _targetTag = inputGameObject.tag;
        _currentComboTrain.Clear();
        _currentComboTrain.Add(inputGameObject.GetComponent<Rigidbody2D>());
        transform.position = inputGameObject.transform.position;
        _hunterEnabled = true;
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody2D comboTrainRb in _currentComboTrain)
        {
            if (comboTrainRb.bodyType == RigidbodyType2D.Dynamic)
            {
                DeactivateHunter();
                return;
            }
        }
        
        foreach (DotBehaviour comboScript in _currentComboTrainScripts)
        {
            if (!comboScript._gravityActivated)
            {
                DeactivateHunter();
                return;
            }
        }
        
        if (!_hunterEnabled) { return;}
        
        if (_currentFrame < _maximumFrames)
        {
            transform.position = _currentComboTrain[_currentIndex].transform.position;
            _currentFrame++;
            _currentIndex++;
            if (_currentIndex >= _currentComboTrain.Count)
            {
                _currentIndex = _baseIndex;
                _maximumFrames -= _baseIndex;
                _baseIndex++;
            }
        }
        else
        {
            CheckComboScore();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hunterEnabled) { return; }
        if (other.gameObject.CompareTag(_targetTag))
        {
            _dotParent = other.transform.parent.GetComponent<Rigidbody2D>();
            if (!_currentComboTrain.Contains(_dotParent))
            {
                if (_dotParent.GetComponent<DotBehaviour>()._gravityActivated)
                {
                    _currentComboTrain.Add(_dotParent);
                    _currentComboTrainScripts.Add(_dotParent.GetComponent<DotBehaviour>());
                    _maximumFrames += _currentComboTrain.Count;
                }
            }
        }
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (!_hunterEnabled) { return; }
        if (other.gameObject.CompareTag(_targetTag))
        {
            _dotParent = other.transform.parent.GetComponent<Rigidbody2D>();
            if (!_currentComboTrain.Contains(_dotParent))
            {
                if (_dotParent.GetComponent<DotBehaviour>()._gravityActivated)
                {
                    _currentComboTrain.Add(_dotParent);
                    _currentComboTrainScripts.Add(_dotParent.GetComponent<DotBehaviour>());
                    _maximumFrames += _currentComboTrain.Count;
                }
            }
        }
    }

    private void CheckComboScore()
    {
        if (_currentComboTrain.Count >= _combosNeededToScore)
        {
            currentComboCount.value++;
            _bombPlanted = false;
            foreach (Rigidbody2D comboDot in _currentComboTrain)
            {
                if (!comboDot.gameObject.activeInHierarchy)
                {
                    DeactivateHunter();
                    return;
                }
            }
            foreach (Rigidbody2D comboDot in _currentComboTrain)
            {
                if (comboDot.name == "Bad Dot")
                {
                    badDotCount.value--;
                    updateBadDotText.Raise();
                    foreach (GameObject badDotParticle in badDotParticlePool)
                    {
                        if (!badDotParticle.activeSelf)
                        {
                            badDotParticle.transform.position = comboDot.transform.position;
                            badDotParticle.SetActive(true);
                            break;
                        }
                    }
                }
                else
                {
                    if (comboDot.name != "Frozen Dot")
                    {
                        foreach (GameObject dotParticle in dotParticlePool)
                        {
                            if (!dotParticle.activeSelf)
                            {
                                dotParticle.transform.position = comboDot.transform.position;
                                dotParticle.SetActive(true);
                                break;
                            }
                        }
                    }
                }

                if (comboDot.name != "Frozen Dot")
                {
                    comboDot.gameObject.SetActive(false);
                    if (currentComboCount.value % _combosNeededForBomb == 0 && _bombPlanted == false)
                    {
                        Instantiate(bomb, comboDot.transform.position, quaternion.identity);
                        _awardAudioSource.Play();
                        _bombPlanted = true;
                    }
                }
                else
                {
                    comboDot.gameObject.GetComponent<CheckObjectActiveBehaviourV1>().CheckObjectState();
                    comboDot.name = "Bad Dot";
                }
            }
            activateGravityEvent.Raise();
        }
        if (badDotCount.value <= 0)
        {
            if (turnsTaken.value < levelPersonalBest.value)
            {
                levelPersonalBest.value = turnsTaken.value;
                personalBestLoader.SavePref();
            }
            levelCompleteEvent.Raise();
        }
        DeactivateHunter();
    }

    private void DeactivateHunter()
    {
        _hunterEnabled = false;
        _currentFrame = 0;
        _currentIndex = 0;
        _maximumFrames = 2;
        _baseIndex = 0;
        gameObject.SetActive(false);
    }
}
