using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;
    public AudioSource typeSound;

    private string fullTextCache; // Guardamos el texto completo
    private Coroutine typingRoutine;

    public void ShowText(string fullText, System.Action onComplete = null)
    {
        fullTextCache = fullText; // Guardamos el texto para SkipToFullText
        //StopAllCoroutines();

        if (typingRoutine != null)
            StopCoroutine(typingRoutine);


        StartCoroutine(TypeText(fullText, onComplete));
    }

    private IEnumerator TypeText(string text, System.Action onComplete)
    {
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter;

            if (typeSound != null && letter != ' ')
                typeSound.PlayOneShot(typeSound.clip);

            yield return new WaitForSeconds(typingSpeed);
        }

        onComplete?.Invoke(); // Avisamos al DialogueManager
    }

    public void SkipToFullText()
    {
        StopAllCoroutines();
        dialogueText.text = fullTextCache; // Mostramos el texto completo
    }
}