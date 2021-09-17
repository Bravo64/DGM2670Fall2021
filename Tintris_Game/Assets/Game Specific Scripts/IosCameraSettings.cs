using UnityEngine;

[RequireComponent(typeof(Camera))]
public class IosCameraSettings : MonoBehaviour
{
    public bool alignShapeCreator = true;
    public Transform shapeCreator;
    public bool alignPauseMenu = false;
    public Transform pauseMenu;
    public bool alignSettingsMenu = false;
    public Transform settingsMenu;
    
    private Camera _myCameraComponent;
    private int[] deviceWidths = new[] {750, 1080, 1125, 828, 1242, 1536, 2048, 1668, 640, 768, 1125};
    private float[] cameraSizes = new[] {13.8f, 13.8f, 16.6f, 16.6f, 16.6f, 10.5f, 10.5f, 10.5f, 13.35f, 10.5f, 16.6f};
    private float[] cameraYPos = new[] {5.05f, 5.05f, 7.9f, 7.9f, 7.9f, 1.65f, 1.65f, 1.65f, 4.8f, 1.65f, 7.9f};
    private float[] shapeCreatorYPos = new[] {19.0f, 19.0f, 24.0f, 24.0f, 24.0f, 12.0f, 12.0f, 12.0f, 18.0f, 12.0f, 24.0f};

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
        if (alignShapeCreator)
        {
            shapeCreator.position = new Vector3(shapeCreator.position.x, shapeCreatorYPos[_deviceIndex], 0);
        }
        if (alignPauseMenu)
        {
            pauseMenu.position = new Vector3(pauseMenu.position.x, pauseMenu.position.y + shapeCreatorYPos[_deviceIndex]/3, 0);
        }
        if (alignSettingsMenu)
        {
            settingsMenu.position = new Vector3(settingsMenu.position.x, settingsMenu.position.y + shapeCreatorYPos[_deviceIndex]/3, 0);
        }
    }
}
