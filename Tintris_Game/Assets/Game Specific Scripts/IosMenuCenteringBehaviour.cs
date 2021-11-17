using UnityEngine;

public class IosMenuCenteringBehaviour : MonoBehaviour
{
    public Transform creditsMenu;
    public Transform settingsMenu;

    private int[] deviceWidths = new[] { 750, 1080, 1125, 828, 1242, 1536, 2048, 1668, 640, 768, 1125 };

    private float[] creditsMenuYPos = new[]
        { 1369.0f, 1369.0f, 1372.0f, 1372.0f, 1372.0f, 1366.0f, 1366.0f, 1366.0f, 1368.0f, 1366.0f, 1374.0f };
    private float[] settingsMenuYPos = new[]
        { 19.0f, 19.0f, 24.0f, 24.0f, 24.0f, 12.0f, 12.0f, 12.0f, 18.0f, 12.0f, 24.0f };

    private int _deviceIndex;

    private void Start()
    {
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
        CenterMenuElements();
    }

    void CenterMenuElements()
    {
        creditsMenu.position = new Vector3(creditsMenu.position.x, creditsMenuYPos[_deviceIndex], 0);
        //settingsMenu.position = new Vector3(creditsMenu.position.x, settingsMenuYPos[_deviceIndex], 0);
    }
}
