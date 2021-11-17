using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CenterInstructionsBehaviour : MonoBehaviour
{

    private Camera _myCameraComponent;
    private int[] deviceWidths = new[] {750, 1080, 1125, 828, 1242, 1536, 2048, 1668, 640, 768, 1125};
    private float[] cameraSizes = new[] {13.8f, 13.8f, 16.6f, 16.6f, 16.6f, 10.5f, 10.5f, 10.5f, 13.35f, 10.5f, 16.6f};
    private float[] cameraYPos = new[] {1.9f, 2.2f, 2.2f, 2.2f, 2.2f, 1.65f, 1.65f, 1.65f, 2.2f, 1.65f, 2.2f};

    private int _deviceIndex;
    
    private void Awake()
    {
        _myCameraComponent = GetComponent<Camera>();
        _deviceIndex = System.Array.IndexOf(deviceWidths, Screen.width);
         #if UNITY_ANDROID
        if (_deviceIndex < 0)
        {
            _deviceIndex = 10;
        }
         #elif UNITY_IOS
        if (_deviceIndex < 0)
        {
            _deviceIndex = 10;
        }
         #elif UNITY_STANDALONE_OSX
        if (_deviceIndex < 0)
        {
            _deviceIndex = 6;
        }
         #elif UNITY_STANDALONE_WIN
        if (_deviceIndex < 0)
        {
            _deviceIndex = 6;
        }
        #endif
        _myCameraComponent.orthographicSize = cameraSizes[_deviceIndex];
        transform.position = new Vector3(transform.position.x, cameraYPos[_deviceIndex], transform.position.z);
    }
}
