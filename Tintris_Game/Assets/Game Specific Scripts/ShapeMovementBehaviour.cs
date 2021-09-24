
using System.Collections;
using GameEvents;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShapeMovementBehaviour : MonoBehaviour
{
    public VoidEvent createNewShape;
    public float dropInterval = 0.75f;
    public GameObject sensorChildren;
    public DotBehaviour[] myBlockScripts;
    public GameObjectEvent parentNewDotEvent;
    public bool lockControls;

    private Vector3 _sprinRight = new Vector3(0, 0, -90);
    private WaitForSeconds _waitForSecondsOBJ1, _waitForSecondsOBJ2;
    private AudioSource _myAudioSource;
    private int _droppedLastFrame, _sideLastFrame = 4;
    private float _dropHoldTime = 0.75f, _rightHoldTime = 0.75f, _leftHoldTime = 0.75f;
    private bool _movementComplete, _processingFreeFall;

    [HideInInspector] public bool rightMovementLocked, leftMovementLocked, 
        groundDetected, swipedDown, moveRightPressed, moveRightHeld, 
        moveLeftPressed, moveLeftHeld, dropPressed, dropHeld, 
        spinRightPressed, spinLeftPressed;
    [HideInInspector] public int dropFrameInterval = 10;

    private void Start()
    {
        _droppedLastFrame = dropFrameInterval;
        _waitForSecondsOBJ1 = new WaitForSeconds(dropInterval);
        _waitForSecondsOBJ2 = new WaitForSeconds(dropInterval / 10);
        _myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitAndDrop());
    }
    
    public void OnFreeFallButtonDown()
    {
        _processingFreeFall = true;
        spinRightPressed = spinLeftPressed = false;
        StartCoroutine(KeepFalling());
    }

    IEnumerator KeepFalling()
    {
        for (int i = 0; i < 20; i++)
        {
            spinRightPressed = spinLeftPressed = false;
            dropPressed = dropHeld = swipedDown = true;
            _dropHoldTime = dropFrameInterval = 0;
            dropInterval = 0.01f;
            yield return 0;
        }
        _processingFreeFall = false;
    }

    private void Update()
    {
        if (lockControls) { return; }

        if (Input.GetButtonDown("MoveRight"))
        {
            moveRightPressed = moveRightHeld = true;
        }
        
        if (Input.GetButtonUp("MoveRight") || !Input.GetButton("MoveRight"))
        {
            moveRightHeld = false;
            _rightHoldTime = 0.75f;
        }
        
        if (Input.GetButtonDown("MoveLeft"))
        {
            moveLeftPressed = moveLeftHeld = true;
        }
        
        if (Input.GetButtonUp("MoveLeft") || !Input.GetButton("MoveLeft"))
        {
            moveLeftHeld = false;
            _leftHoldTime = 0.75f;
        }
        
        if (Input.GetButtonDown("Drop"))
        {
            dropPressed = dropHeld = true;
        }
        
        if (Input.GetButtonUp("Drop") || !Input.GetButton("Drop") && !swipedDown)
        {
            dropHeld = false;
            _dropHoldTime = 0.75f;
        }
        
        if (Input.GetButtonDown("SpinRight"))
        {
            spinRightPressed = true;
        }
        
        if (Input.GetButtonDown("SpinLeft"))
        {
            spinLeftPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (lockControls) { return; }
        
        if (_movementComplete)
        {
            _movementComplete = false;
        }

        if (moveRightPressed)
        {
            if (!rightMovementLocked)
            {
                groundDetected = leftMovementLocked = false;
                transform.position += Vector3.right;
            }
            moveRightPressed = false;
        }

        if (moveLeftPressed)
        {
            if (!leftMovementLocked)
            {
                groundDetected = rightMovementLocked = false;
                transform.position += Vector3.left;
            }
            moveLeftPressed = false;
        }

        if (moveRightHeld && !rightMovementLocked && !_movementComplete)
        {
            _rightHoldTime -= Time.deltaTime;
            if (_rightHoldTime < 0.0f)
            {
                if (_sideLastFrame < 0)
                {
                    transform.position += Vector3.right;
                    groundDetected = leftMovementLocked = false;
                    _sideLastFrame = 4;
                }
                else
                {
                    _sideLastFrame--;
                }
            }
            _movementComplete = true;
        }
        
        if (moveLeftHeld && !leftMovementLocked && !_movementComplete)
        {
            _leftHoldTime -= Time.deltaTime;
            if (_leftHoldTime < 0.0f)
            {
                if (_sideLastFrame < 0)
                {
                    transform.position += Vector3.left;
                    groundDetected = rightMovementLocked = false;
                    _sideLastFrame = 4;
                }
                else
                {
                    _sideLastFrame--;
                }
            }
            _movementComplete = true;
        }

        if (spinRightPressed && !_movementComplete && !_processingFreeFall)
        {
            transform.eulerAngles += _sprinRight;
                foreach (DotBehaviour dot in myBlockScripts)
                {
                    dot.transform.eulerAngles -= _sprinRight;
                }
                rightMovementLocked = leftMovementLocked = groundDetected = spinRightPressed = false;
                _movementComplete = true;
        }
        
        if (spinLeftPressed && !_movementComplete && !_processingFreeFall)
        {
            transform.eulerAngles -= _sprinRight;
                foreach (DotBehaviour dot in myBlockScripts)
                {
                    dot.transform.eulerAngles += _sprinRight;
                }
                rightMovementLocked = leftMovementLocked = groundDetected = spinLeftPressed = false;
                _movementComplete = true;
        }
        
        if (dropPressed && !groundDetected && !_movementComplete)
        {
            transform.position += Vector3.down;
            rightMovementLocked = leftMovementLocked = dropPressed = false;
            _movementComplete = true;
        }
        
        if (dropHeld && !groundDetected && !_movementComplete)
        {
            _dropHoldTime -= Time.deltaTime;
            if (_dropHoldTime < 0.0f || swipedDown)
            {
                if (_droppedLastFrame < 0)
                {
                    transform.position += Vector3.down;
                    rightMovementLocked = leftMovementLocked = false;
                    _movementComplete = true;
                    _droppedLastFrame = dropFrameInterval;
                }
                else
                {
                    _droppedLastFrame--;
                }
            }
        }

        moveRightPressed = moveLeftPressed = spinRightPressed = spinLeftPressed = dropPressed = false;
    }

    IEnumerator WaitAndDrop()
    {
        while (true)
        {
            if (swipedDown || dropHeld)
            {
                yield return _waitForSecondsOBJ2;
            }
            else
            {
                yield return _waitForSecondsOBJ1;
            }
            if (!groundDetected)
            {
                transform.position += Vector3.down;
                rightMovementLocked = leftMovementLocked = false;
            }
            else
            {
                lockControls = true;
                yield return 2;
                while (!groundDetected)
                {
                    yield return 3;
                    transform.position += Vector3.down;
                }
                _myAudioSource.Play();
                foreach (DotBehaviour blockScript in myBlockScripts)
                {
                    if (blockScript.gameObject.activeSelf)
                    {
                        blockScript.gameObject.layer = LayerMask.NameToLayer("Landed Dot");
                        blockScript.ShapeLandedEvent();
                        parentNewDotEvent.Raise(blockScript.gameObject);
                    }
                    else
                    {
                        Destroy(blockScript.gameObject);
                    }
                }
                sensorChildren.SetActive(false);
                createNewShape.Raise();
                StopAllCoroutines();
                Destroy(gameObject, 0.4f);
            }
            _movementComplete = moveLeftPressed = moveRightPressed = false;
        }
    }
}
