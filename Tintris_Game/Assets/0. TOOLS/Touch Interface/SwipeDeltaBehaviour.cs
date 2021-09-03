using UnityEngine;

public class SwipeDeltaBehaviour : MonoBehaviour
{
    private float _swipeMagnitude;
    private Vector2 startTouch, swipeDelta;


    private void Update()
    {
        swipeDelta = Vector2.zero;
        #if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
        #elif UNITY_IOS || UNITY_ANDROID
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    startTouch = Input.touches[0].position;
                }
                else
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }
            }

#endif
    }
}
