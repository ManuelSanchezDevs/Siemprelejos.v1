using System;
using TMPro;
using UnityEngine;
using static PlayerData;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Rendering.MaterialUpgrader;
using static UnityEngine.EventSystems.EventTrigger;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public DialogueManager dialogueManager;
    public Gender gender;
    public FadePanel fadePanel;
    

    private void Start()
    {
        ShowGretting();
    }

    public void ShowGretting()
    {
        string pronombre = gender.GetPronoum();

        string playerName = PlayerData.Instance.playerName;

        string frase = "  "  + pronombre + " joven " + playerName + " avanzaba entre los arboles," +
            " sintiendo cómo las raíces vibraban bajo sus pies." +
            " El bosque le hablaba en susurros rotos, como si estuviera enfermo." +
            " Un olor a aceite quemado flotaba en el aire, impropio de aquel lugar sagrado. " +
            "Mientras apartaba unas ramas para seguir el rastro de la corrupción," +
            " un autómata oxidado emergió de entre los matorrales y se lanzó a atacarla.";
        //typewriter.ShowText(frase);
        dialogueManager.StartDialogue(frase);
    }
}
