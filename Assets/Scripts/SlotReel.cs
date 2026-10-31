using System.Collections;
using UnityEngine;

public class SlotReel : MonoBehaviour
{
    public SpriteRenderer symbolRenderer;
    public SlotMachineManager gameManager;

    public float spinDuration = 2f;
    public float switchInterval = 0.08f;

    private bool isSpinning;
    public bool IsSpinning => isSpinning;
    public void SetRandomSymbol()
    {
        symbolRenderer.sprite = gameManager.GetRandomSymbol();
    }

    public void StartSpin()
    {
        if (!isSpinning)
        {
            StartCoroutine(Spin());
        }
    }

    private IEnumerator Spin()
    {
        isSpinning = true;

        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            SetRandomSymbol();

            yield return new WaitForSeconds(switchInterval);
            elapsedTime += switchInterval;
        }

        SetRandomSymbol();
        isSpinning = false;
    }

    [ContextMenu("Test Random Symbol")]
    private void TestRandomSymbol()
    {
        SetRandomSymbol();
    }

}