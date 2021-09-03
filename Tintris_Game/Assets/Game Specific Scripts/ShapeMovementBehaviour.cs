
using System.Collections;
using GameEvents;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShapeMovementBehaviour : MonoBehaviour
{
    public VoidEvent createNewShape;
    public float dropInterval = 1.0f;
    public GameObject sensorChildren;
    public DotBehaviour[] myBlockScripts;
    public GameObjectEvent parentNewDotEvent;
    public bool lockControls = false;

    [HideInInspector] public bool rightMovementLocked, leftMovementLocked, groundDetected;

    private Vector3 _sprinRight = new Vector3(0, 0, -90);
    private WaitForSeconds _waitForSecondsOBJ1;
    private WaitForSeconds _waitForSecondsOBJ2;
    private AudioSource _myAudioSource;
    private int _droppedLastFrame;
    private int _sideLastFrame = 4;
    private float _dropHoldTime = 0.75f;
    private float _rightHoldTime = 0.75f;
    private float _leftHoldTime = 0.75f;
    private bool _movementComplete = false;

    [HideInInspector] public bool moveRightPressed = false;
    [HideInInspector] public bool moveRightHeld = false;
    [HideInInspector] public bool moveLeftPressed = false;
    [HideInInspector] public bool moveLeftHeld = false;
    [HideInInspector] public bool dropPressed = false;
    [HideInInspector] public bool dropHeld = false;
    [HideInInspector] public bool spinRightPressed = false;
    [HideInInspector] public bool spinLeftPressed = false;
    [HideInInspector] public int dropFrameInterval = 10;
    
    [HideInInspector] public bool swipedDown = false;

    private void Start()
    {
        _droppedLastFrame = dropFrameInterval;
        _waitForSecondsOBJ1 = new WaitForSeconds(dropInterval);
        _waitForSecondsOBJ2 = new WaitForSeconds(dropInterval / 10);
        _myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitAndDrop());
    }

    private void Update()
    {
        if (lockControls) { return; }
        
        if (Input.GetButtonDown("MoveRight"))
        {
            moveRightPressed = true;
            moveRightHeld = true;
        }
        
        if (Input.GetButtonUp("MoveRight") || !Input.GetButton("MoveRight"))
        {
            moveRightHeld = false;
            _rightHoldTime = 0.75f;
        }
        
        if (Input.GetButtonDown("MoveLeft"))
        {
            moveLeftPressed = true;
            moveLeftHeld = true;
        }
        
        if (Input.GetButtonUp("MoveLeft") || !Input.GetButton("MoveLeft"))
        {
            moveLeftHeld = false;
            _leftHoldTime = 0.75f;
        }
        
        if (Input.GetButtonDown("Drop"))
        {
            dropPressed = true;
            dropHeld = true;
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
                groundDetected = false;
                transform.position += Vector3.right;
                leftMovementLocked = false;
            }
            moveRightPressed = false;
        }

        if (moveLeftPressed)
        {
            if (!leftMovementLocked)
            {
                groundDetected = false;
                transform.position += Vector3.left;
                rightMovementLocked = false;
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
                    groundDetected = false;
                    leftMovementLocked = false;
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
                    groundDetected = false;
                    rightMovementLocked = false;
                    _sideLastFrame = 4;
                }
                else
                {
                    _sideLastFrame--;
                }
            }
            _movementComplete = true;
        }

        if (spinRightPressed && !_movementComplete)
        {
            transform.eulerAngles += _sprinRight;
            foreach (DotBehaviour dot in myBlockScripts)
            {
                dot.transform.eulerAngles -= _sprinRight;
            }
            rightMovementLocked = false;
            leftMovementLocked = false;
            groundDetected = false;
            _movementComplete = true;
            spinRightPressed = false;
        }
        
        if (spinLeftPressed && !_movementComplete)
        {
            transform.eulerAngles -= _sprinRight;
            foreach (DotBehaviour dot in myBlockScripts)
            {
                dot.transform.eulerAngles += _sprinRight;
            }
            rightMovementLocked = false;
            leftMovementLocked = false;
            groundDetected = false;
            _movementComplete = true;
            spinLeftPressed = false;
        }
        
        if (dropPressed && !groundDetected && !_movementComplete)
        {
            transform.position += Vector3.down;
            rightMovementLocked = false;
            leftMovementLocked = false;
            _movementComplete = true;
            dropPressed = false;
        }
        
        if (dropHeld && !groundDetected && !_movementComplete)
        {
            _dropHoldTime -= Time.deltaTime;
            if (_dropHoldTime < 0.0f || swipedDown)
            {
                if (_droppedLastFrame < 0)
                {
                    transform.position += Vector3.down;
                    rightMovementLocked = false;
                    leftMovementLocked = false;
                    _movementComplete = true;
                    _droppedLastFrame = dropFrameInterval;
                }
                else
                {
                    _droppedLastFrame--;
                }
            }
        }

        moveRightPressed = false;
        moveLeftPressed = false;
        spinRightPressed = false;
        spinLeftPressed = false;
        dropPressed = false;
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
                rightMovementLocked = false;
                leftMovementLocked = false;
            }
            else
            {
                lockControls = true;
                yield return 2;
                while (!groundDetected)
                {
                    yield return 1;
                    transform.position += Vector3.down;
                    yield return 1;
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
            _movementComplete = false;
            moveLeftPressed = false;
            moveRightPressed = false;
        }
    }
}
