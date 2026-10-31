using System.Collections;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    public Sprite[] symbols;
    public SlotReel[] reels;

    public int balance = 100;
    public int spinCost = 10;
    public int winPayout = 50;

    public TMPro.TextMeshProUGUI balanceText;
    public TMPro.TextMeshProUGUI resultText;

    private bool isGameRunning;

    private void Start()
    {
        UpdateBalanceUI();
        resultText.text = "";
    }

    public void SpinAllReels()
    {
        if (isGameRunning)
            return;

        if (balance < spinCost)
        {
            Debug.Log("Insufficient balance!");
            return;
        }

        balance -= spinCost;
        UpdateBalanceUI();

        isGameRunning = true;
        resultText.text = "";

        foreach (SlotReel reel in reels)
        {
            reel.StartSpin();
        }

        StartCoroutine(CheckResultsWhenStopped());
    }

    private IEnumerator CheckResultsWhenStopped()
    {
        while (true)
        {
            bool anySpinning = false;

            foreach (SlotReel reel in reels)
            {
                if (reel.IsSpinning)
                {
                    anySpinning = true;
                    break;
                }
            }

            if (!anySpinning)
                break;

            yield return null;
        }

        CheckWin();
        isGameRunning = false;
    }

    public Sprite GetRandomSymbol()
    {
        int randomIndex = Random.Range(0, symbols.Length);
        return symbols[randomIndex];
    }

    [ContextMenu("Test Spin All Reels")]
    private void TestSpinAllReels()
    {
        SpinAllReels();
    }

    public void CheckWin()
    {
        Sprite first = reels[0].symbolRenderer.sprite;
        Sprite second = reels[1].symbolRenderer.sprite;
        Sprite third = reels[2].symbolRenderer.sprite;

        if (first == second && second == third)
        {
            balance += winPayout;
            resultText.text = "JACKPOT! +" + winPayout;
            Debug.Log("JACKPOT! You won " + winPayout + " credits!");
        }
        else
        {
            resultText.text = "TRY AGAIN!";
            Debug.Log("No win. Try again!");
        }

        UpdateBalanceUI();
        Debug.Log("Current Balance: " + balance);
    }

    private void UpdateBalanceUI()
    {
        balanceText.text = "Balance: " + balance;
    }

    public GameObject exitConfirmationPanel;

    public void ShowExitConfirmation()
    {
        exitConfirmationPanel.SetActive(true);
    }

    public void CancelExit()
    {
        exitConfirmationPanel.SetActive(false);
    }

    public void ConfirmExit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}