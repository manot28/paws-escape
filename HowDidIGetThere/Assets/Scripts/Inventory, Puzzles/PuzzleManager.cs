using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private MovePuzzle[] puzzlePieces;

    // a list of game objects to activate when the door is unlocked
    [SerializeField]
    private GameObject[] doorOpenObjs;

    [SerializeField]
    private GameObject doorClosed;

    private AudioManager audioManager;

    private bool completed;

    private void Awake()
    {
        audioManager = GameObject.Find("IDLE_0").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (completed)
            return;

        foreach (MovePuzzle piece in puzzlePieces)
        {
            if (!piece.finishPos)
            {
                return;
            }
        }

        completed = true;
        foreach(GameObject obj in doorOpenObjs)
            obj.SetActive(true);


        if(audioManager != null) 
            audioManager.PlaySound("door");
        doorClosed.SetActive(false);
    }
}