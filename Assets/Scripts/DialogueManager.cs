using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public TypewriterEffect typewriter;
    public TextMeshProUGUI dialogueText;
    public GameObject nextIndicator; // El icono ▶
    public float typingSpeed = 0.05f;

    private List<string> pages;
    private int currentPage = 0;
    private bool isTyping = false;

    private void Start()
    {
        nextIndicator.SetActive(false);
        QualitySettings.vSyncCount = 1;   // Activa VSync
        Application.targetFrameRate = 60; // Limita a 60 FPS
    }

    public void StartDialogue(string fullText)
    {
        pages = SplitIntoPages(fullText, 70); // 180 caracteres por página aprox
        currentPage = 0;
        ShowPage();
    }

    private void ShowPage()
    {
        nextIndicator.SetActive(false);
        isTyping = true;

        typewriter.typingSpeed = typingSpeed;
        typewriter.ShowText(pages[currentPage], OnTypingComplete);
    }

    private void OnTypingComplete()
    {
        isTyping = false;
        nextIndicator.SetActive(true);
    }

    private List<string> SplitIntoPages(string text, int maxChars)
    {
        List<string> result = new List<string>();
        string[] words = text.Split(' ');

        string current = "";

        foreach (string w in words)
        {
            if ((current + w).Length > maxChars)
            {
                result.Add(current.Trim());
                current = "";
            }
            current += w + " ";
        }

        if (current.Trim().Length > 0)
            result.Add(current.Trim());

        return result;
    }

    public void OnNextButtonPressed()
    {
        if (pages == null) return;

        if (isTyping)
        {
            typewriter.SkipToFullText();
            return;
        }

        if (currentPage < pages.Count - 1)
        {
            currentPage++;
            ShowPage();
        }
        else
        {
            dialogueText.text = "";
            nextIndicator.SetActive(false);
        }
    }
}