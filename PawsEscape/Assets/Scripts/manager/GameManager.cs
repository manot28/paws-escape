using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Keys { get; private set; }

    private const string KEYS_SAVE = "KEYS_SAVE";
    private const string HAS_SAVE = "HAS_SAVE";

    private const string MUSIC_VOLUME = "MUSIC_VOLUME";
    private const string SFX_VOLUME = "SFX_VOLUME";

    public float MusicVolume { get; private set; } = 1f;
    public float SfxVolume { get; private set; } = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadGame();
        }
        else
            Destroy(gameObject);
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, value);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float value)
    {
        SfxVolume = value;
        PlayerPrefs.SetFloat(SFX_VOLUME, value);
        PlayerPrefs.Save();
    }

    public void AddKey(int amount = 1)
    {
        Keys += amount;
        SaveGame();
    }

    public bool UseKeys(int amount)
    {
        if (Keys < amount)
            return false;

        Keys -= amount;
        SaveGame();

        return true;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt(KEYS_SAVE, Keys);
        PlayerPrefs.SetInt(HAS_SAVE, 1);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        Time.timeScale = 1;

        Keys = PlayerPrefs.GetInt(KEYS_SAVE, 0);

        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f);
        SfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME, 1f);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey(KEYS_SAVE);
        PlayerPrefs.DeleteKey(HAS_SAVE);

        Keys = 0;
        MusicVolume = 1f;
        SfxVolume = 1f;

        Time.timeScale = 1;
        SceneManager.LoadScene("load");
    }

    public void ContinueGame()
    {
        if (!HasSaveGame())
        {
            Debug.Log("No save found");
            return;
        }

        LoadGame();
        SceneManager.LoadScene("Lobby");
    }

    public bool HasSaveGame()
    {
        return PlayerPrefs.GetInt(HAS_SAVE, 0) == 1;
    }
}