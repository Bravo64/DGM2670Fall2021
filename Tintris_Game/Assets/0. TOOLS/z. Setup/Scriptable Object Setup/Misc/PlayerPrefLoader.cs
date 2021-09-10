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
    
    public void LoadPref()
    {
        if (attachSceneIndexToKey)
        {
            _completeKey = playerPrefSaveKey + SceneManager.GetActiveScene().buildIndex.ToString();
        }
        else
        {
            _completeKey = playerPrefSaveKey;
        }
        PlayerPrefs.SetInt (playerPrefSaveKey, prefIntDataObj.value);
        prefIntDataObj.value = PlayerPrefs.GetInt(_completeKey, 0);
    }
}
