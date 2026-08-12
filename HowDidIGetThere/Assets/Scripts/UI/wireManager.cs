using UnityEngine;
using UnityEngine.SceneManagement;

public class WireManager : MonoBehaviour
{
    [SerializeField]
    private Wire[] wires;

    [SerializeField]
    private GameObject[] objsActive;

    [SerializeField]
    private GameObject menuOff;


    private bool completed;

    private void Update()
    {
        if (completed)
            return;

        foreach (Wire wire in wires)
        {
            if (!wire.finishPos)
                return;
        }

        completed = true;
        foreach (GameObject door in objsActive)
            door.SetActive(true);
        menuOff.SetActive(false);
    }
}