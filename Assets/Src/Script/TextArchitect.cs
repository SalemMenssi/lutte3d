using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect
{
    private TextMeshPro tmproUI;
    private TextMeshProUGUI tmproUGUI;
    public TMP_Text tmpro => tmproUI != null ? tmproUI : tmproUGUI;

    public string CurrentText => tmpro.text;
    public string targetText { get; private set; } = "";

    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }
    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private const float baseSpeed = 1;
    private float speedMultiplier = 1;

    public int charactersPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; } }
    private int characterMultiplier = 1;

    public bool hurryUp = false;

    public TextArchitect(TextMeshPro tmpro)
    {
        this.tmproUI = tmpro;
    }
    public TextArchitect(TextMeshProUGUI tmpro)
    {
        this.tmproUGUI = tmpro;
    }

    public Coroutine Build(string text)
    {
        targetText = text;

        Stop();
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;

    public bool isBuilding => buildProcess != null;
    public void Stop()
    {
        if (!isBuilding)
        {
            return;
        }
        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    private IEnumerator Building()
    {
        PrepareTypeWriter();
        yield return BuildingTypeWriter();
        OnComplete();
    }

    private IEnumerator BuildingTypeWriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += hurryUp ? charactersPerCycle * 3 : charactersPerCycle;
            yield return new WaitForSeconds(0.015f / speed);
        }
    }

    private void PrepareTypeWriter()
    {
        tmpro.color = tmpro.color; // Ensure the color is set correctly
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = targetText;
        tmpro.ForceMeshUpdate();
    }

    private void OnComplete()
    {
        buildProcess = null;
        hurryUp = false;
    }

    public void ForceComplete()
    {
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        Stop();
        OnComplete();
    }
}
