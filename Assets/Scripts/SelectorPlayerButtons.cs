using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class SelectorPlayerButtons : MonoBehaviour
{
    public TMPro.TextMeshProUGUI texto; // texto bloque inferior
    [SerializeField] private GameObject playerNameText; // contenedor de input field
    [SerializeField] private GameObject[] panelSelector;
    public TMP_InputField playerName; // input field
    public RectTransform panelButtonsTransform;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    public GameObject[] player;

    private Vector2 startPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    void Start()
    {
        startPos = panelButtonsTransform.anchoredPosition;
        texto = GameObject.Find("TextImageBorder").GetComponent<TextMeshProUGUI>();
        playerNameText = GameObject.Find("PlayerName");
        playerName = playerNameText.GetComponentInChildren<TMP_InputField>();
        playerNameText.SetActive(false);
    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * speed, distance) - (distance / 2f);
        panelButtonsTransform.anchoredPosition = startPos + new Vector2(x, 0);
    }

    public void NewText()
    {
        texto.text = "Escribe tu nombre y pulsa el boton";
        foreach (GameObject player in panelSelector)
        {
            player.SetActive(false);
        }
        playerNameText.SetActive(true);
        StartCoroutine(ActivateInput());
    }

    public IEnumerator ActivateInput()
    {
        yield return null;
        playerName.Select(); 
        playerName.ActivateInputField(); 
    }

    public void DisableText()
    {
        texto.gameObject.SetActive(false);
    }
}
