using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;


public class IntroManager : MonoBehaviour
{
    private RectTransform dialogueBox;
    public float slideDuration = 0.5f;
    public float hiddenY = -290;
    public float shownY = 290;
    public bool isVisible;
    public CanvasGroup blackScreen;
    private Coroutine currentCoroutine;

    [SerializeField]
    private SceneController sceneController;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private GameObject[] Backgrounds;
    

    private TextArchitect textArchitect;

    private List<DialogueElement> dialogueElements;
    private int currentTextIndex = 0;

    private void Awake()
    {
        dialogueBox = GetComponent<RectTransform>();
        textArchitect = new TextArchitect(dialogueText);
    }

    void Start()
    {
        isVisible = false;

        dialogueElements = new List<DialogueElement>
        {
            
            new DialogueElement("Under the blazing sun", actionCoroutine: fadeAndActivateBackground(0)),
            new DialogueElement("A lone warrior trains by the crashing waves", null),
            new DialogueElement("Dreaming of glory", null),
            new DialogueElement("In the village", actionCoroutine: fadeAndActivateBackground(1)),
            new DialogueElement("Whispers grow as he prepares for the legendary tournament", null),
            new DialogueElement("Battle after battle", actionCoroutine: fadeAndActivateBackground(2)),
            new DialogueElement("He defeats fierce opponents", null),
            new DialogueElement("Earning his place in the final", null),
            new DialogueElement("The final duel begins", actionCoroutine: fadeAndActivateBackground(3)),
            new DialogueElement("strength clashes with strategy as the crowd watches in awe", null),
            new DialogueElement("With a final move", actionCoroutine: fadeAndActivateBackground(4)),
            new DialogueElement("A legend is born", null),
           

        };
        StartCoroutine(DisplayNextText());
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textArchitect.isBuilding)
            {
                textArchitect.hurryUp = true;
            }
            else
            {
                StartCoroutine(DisplayNextText());
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            textArchitect.hurryUp = false;
        }
    }

    private IEnumerator DisplayNextText()
    {
        if (dialogueElements.Count == 0) yield break;

        if (currentTextIndex >= dialogueElements.Count)
        {
            sceneController.LoadScene("FightScene");
        }

        DialogueElement currentElement = dialogueElements[currentTextIndex];

        if (currentElement.ActionCoroutine != null)
        {
            yield return StartCoroutine(currentElement.ActionCoroutine);
        }

        textArchitect.Build(currentElement.Text);
        currentTextIndex++;
    }

    private IEnumerator fadeAndActivateBackground(int backgroundIndex)
    {
        if (blackScreen.alpha == 1)
        {
            disActivateAllBGs();
            Backgrounds[backgroundIndex].gameObject.SetActive(true);
            blackScreen.DOFade(0f, 1f);
            yield return null;
        }
        else if (blackScreen.alpha == 0)
        {
            blackScreen.DOFade(1f, 1f); 
            yield return new WaitForSeconds(1f); 
            disActivateAllBGs();
            Backgrounds[backgroundIndex].gameObject.SetActive(true);
            blackScreen.DOFade(0f, 1f);
            yield return null;
        }
        yield return null;
    }

    

    private void disActivateAllBGs()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            Backgrounds[i].gameObject.SetActive(false);
        }
    }
}
