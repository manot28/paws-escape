using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [System.Serializable]
    private class Hotkey
    {
        public KeyCode key;
        public Button button;
    }

    [SerializeField]
    private List<Hotkey> hotkeys = new();

    private Dictionary<KeyCode, Button> hotkeyDictionary;

    private void Awake()
    {
        hotkeyDictionary = new Dictionary<KeyCode, Button>();

        foreach (Hotkey hotkey in hotkeys)
        {
            if (!hotkeyDictionary.ContainsKey(hotkey.key))
                hotkeyDictionary.Add(hotkey.key, hotkey.button);

            else
                Debug.LogWarning($"ðotkey {hotkey.key} is already assigned");
        }
    }

    private void Update()
    {
        foreach (var pair in hotkeyDictionary)
        {
            if (Input.GetKeyDown(pair.Key))
                pair.Value.onClick.Invoke();
        }
    }

    public void NewGame()
    {
        GameManager.Instance.NewGame();
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Save()
    {
        GameManager.Instance.SaveGame();
    }

    public void Load()
    {
        GameManager.Instance.LoadGame();
    }
}