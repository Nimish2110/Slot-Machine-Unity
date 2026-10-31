using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject exitConfirmationPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("SlotMachineScene");
    }
}