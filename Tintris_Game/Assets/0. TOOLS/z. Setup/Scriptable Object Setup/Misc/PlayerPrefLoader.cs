using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Player Pref Loader Obj", menuName = "Misc/Player Pref Obj")]
public class PlayerPrefLoader : ScriptableObject
{
    public IntData prefIntDataObj;
    public bool attachSceneIndexToKey;
    public string playerPrefSaveKey;
    
    private string _completeKey;
    
    public void SavePref()
    {
        if (attachSceneIndexToKey)
        {
            _completeKey = playerPrefSaveKey + SceneManager.GetActiveScene().buildIndex.ToString();
        }
        else
        {
            _completeKey = playerPrefSaveKey;
        }
        PlayerPrefs.SetInt (_completeKey, prefIntDataObj.value);
    }
    
    public void LoadPref(int defaultValue)
    {
        if (attachSceneIndexToKey)
        {
            _completeKey = playerPrefSaveKey + SceneManager.GetActiveScene().buildIndex.ToString();
        }
        else
        {
            _completeKey = playerPrefSaveKey;
        }
        prefIntDataObj.value = PlayerPrefs.GetInt(_completeKey, defaultValue);
    }
}
