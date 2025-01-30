using System.Collections;

public class DialogueElement
{


    public string Text { get; }
    public IEnumerator ActionCoroutine { get; }



    public DialogueElement(string text, IEnumerator actionCoroutine)
    {
        Text = text;

        ActionCoroutine = actionCoroutine;
    }


}