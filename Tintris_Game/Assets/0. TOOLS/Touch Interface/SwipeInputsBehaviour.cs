using UnityEngine;

public class SwipeInputsBehaviour : MonoBehaviour
{
    public ShapeMovementBehaviour shapeScript;
    
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, simpleTap, isDragging, justSwiped;
    private Vector2 startTouch, swipeDelta;
    private float _swipePixelLength = 15.0f;
    private float screenWidthDivisor = 25.0f;
    private bool leftRightDrag = false;
    private float tapHoldTime = 0.0f;
    private float dragHoldTime = 0.0f;
    private int slowDropFrameSpeed = 65;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SimpleTap { get { return simpleTap; } }


    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = simpleTap = false;

#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
            shapeScript.swipedDown = false;
        }
        else if (Input.GetMouseButton(0))
        {
            tapHoldTime += Time.deltaTime;
            dragHoldTime += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (shapeScript.dropFrameInterval == slowDropFrameSpeed)
            {
                shapeScript.swipedDown = false;
            }
            isDragging = false;
            if (swipeDelta.magnitude < _swipePixelLength && !justSwiped)
            {
                simpleTap = true;
                if (!swipeDown && !swipeLeft && !swipeRight && !shapeScript.moveLeftPressed && !shapeScript.moveRightPressed)
                {
                    if (!leftRightDrag && !shapeScript.swipedDown)
                    {
                        if (Input.mousePosition.x > Screen.width/2.0f)
                        {
                            if (tapHoldTime <= 0.8f)
                            {
                                shapeScript.spinRightPressed = true;
                            }
                            tapHoldTime = 0.0f;
                        }
                        else
                        {
                            if (tapHoldTime <= 0.8f)
                            {
                                shapeScript.spinLeftPressed = true;
                            }
                            tapHoldTime = 0.0f;
                        }
                    }
                    else
                    {
                        leftRightDrag = false;
                    }
                }
            }
            justSwiped = false;
            Reset();
        }
        #endif
        
        if (Input.touches.Length > 0)
            {
                tapHoldTime += Time.deltaTime;
                dragHoldTime += Time.deltaTime;
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    shapeScript.swipedDown = false;
                    tap = true;
                    isDragging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    if (shapeScript.dropFrameInterval == slowDropFrameSpeed)
                    {
                        leftRightDrag = false;
                        shapeScript.swipedDown = false;
                    }
                    isDragging = false;
                    if (swipeDelta.magnitude < _swipePixelLength && !justSwiped && Input.touches[0].phase == TouchPhase.Ended)
                    {
                        simpleTap = true;
                        if (!swipeDown && !swipeLeft && !swipeRight && !shapeScript.moveLeftPressed && !shapeScript.moveRightPressed)
                        {
                            if (!leftRightDrag && !shapeScript.swipedDown)
                            {
                                if (Input.touches[0].position.x > Screen.width/2.0f)
                                {
                                    if (tapHoldTime <= 0.85f)
                                    {
                                        if (!shapeScript.freeFallActivated)
                                        {
                                            shapeScript.spinRightPressed = true;
                                        }
                                    }
                                    tapHoldTime = 0.0f;
                                }
                                else
                                {
                                    if (tapHoldTime <= 0.85f)
                                    {
                                        if (!shapeScript.freeFallActivated)
                                        {
                                            shapeScript.spinLeftPressed = true;
                                        }
                                    }
                                    tapHoldTime = 0.0f;
                                }
                            }
                            else
                            {
                                leftRightDrag = false;
                            }
                        }
                    }
                    justSwiped = false;
                    Reset();
                }
            }
            else
            {
                leftRightDrag = false;
            }

        swipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touches.Length > 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

        float x = swipeDelta.x;
        float y = swipeDelta.y;

        if (swipeDelta.magnitude > _swipePixelLength)
        {
            simpleTap = false;
            if (Mathf.Abs(x) > Mathf.Abs(y) + 5 || leftRightDrag)
            {
                if (Mathf.Abs(x) > (Screen.width / screenWidthDivisor))
                {
                    if (x < 0)
                    {
                        swipeLeft = true;
                        shapeScript.moveLeftPressed = true;
                    }
                    else
                    {
                        swipeRight = true;
                        shapeScript.moveRightPressed = true;
                    }
                    startTouch += swipeDelta;
                    leftRightDrag = true;
                    shapeScript.spinLeftPressed = false;
                    shapeScript.spinRightPressed = false;
                    shapeScript.swipedDown = false;
                }
            }
            else if (Mathf.Abs(y) > Mathf.Abs(x) + 5)
            {
                if (y < 0)
                {
                    if (!leftRightDrag)
                    {
                        if (dragHoldTime > 0.7f)
                        {
                            shapeScript.dropFrameInterval = slowDropFrameSpeed;
                        }
                        else
                        {
                            shapeScript.dropFrameInterval = 1;
                        }
                        swipeDown = true;
                        shapeScript.dropHeld = true;
                        shapeScript.swipedDown = true;
                        shapeScript.moveRightPressed = false;
                        shapeScript.moveLeftPressed = false;
                        shapeScript.spinRightPressed = false;
                        shapeScript.spinLeftPressed = false;
                        dragHoldTime = 0.0f;
                    }
                }
                else
                {
                    swipeUp = true;
                }
                justSwiped = true;
                Reset();
            }
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
