using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectorPlayerButtons : MonoBehaviour
{
    public TMPro.TextMeshProUGUI texto; // texto bloque inferior
    public TMP_InputField playerName; // input field

    [SerializeField] private GameObject playerNameText; // contenedor de input field
    [SerializeField] private GameObject[] panelSelector;
    public GameObject[] player;
   
    public RectTransform panelButtonsTransform;
    private Vector2 startPos;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
   
    public GameObject fadePanel;

    private Button changeSceneButton;

    private void Awake()
    {
        fadePanel = GameObject.Find("Panel");
        fadePanel.SetActive(false);
        player = GameObject.FindGameObjectsWithTag("Player");
        changeSceneButton = GameObject.Find("Button").GetComponent<Button>();
    }

    void Start()
    {
        startPos = panelButtonsTransform.anchoredPosition;
        texto = GameObject.Find("TextImageBorder").GetComponent<TextMeshProUGUI>();
        playerNameText = GameObject.Find("PlayerName");
        playerName = playerNameText.GetComponentInChildren<TMP_InputField>();
        playerNameText.SetActive(false);
        playerName.onValueChanged.AddListener(CheckName);
        changeSceneButton.interactable = false;
    }

    private void CheckName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            changeSceneButton.interactable = false;
        }
        else
        {
            changeSceneButton.interactable = true;
        }
    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * speed, distance) - (distance / 2f);
        panelButtonsTransform.anchoredPosition = startPos + new Vector2(x, 0);
    }

    public IEnumerator NewText(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    public void NewText()
    {
        texto.text = "Escribe tu nombre y pulsa la flecha";
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
        fadePanel.SetActive(true);
        StartCoroutine(ChangeScene(2f));
    }

    public IEnumerator ChangeScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayerData.Instance.PlayerName = playerName.text;
        SceneManager.LoadScene(2);
    }

    public void SelectBoy()
    {
        SelectCharacter("Boy", Sex.Boy);
    }

    public void SelectGirl()
    {
        SelectCharacter("Girl", Sex.Girl);
    }

    private void SelectCharacter(string characterName, Sex sex)
    {
        PlayerData.Instance.SelectedCharacter = characterName;
        PlayerData.Instance.sex = sex;
    }
}
