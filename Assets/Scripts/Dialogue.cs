using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private Gender gender;
    [SerializeField]private int _index = 0; 
    public int Index { get { return _index; } set { _index = value; } }
    public float espera;

    private List<string> text = new List<string>()
    {
        "La aldea de siemprelejos esta amenazada ",
        "Las tribus salvajes del norte invaden sus territorios",
        "un mal desconocido los esta expulsando de sus tierras",
        " PRONOMBRE joven PLAYERNAME avanzaba entre los arboles buscando OBJETIVO.",
        " ELELLA pensaba que podria mejorar armas para defender la aldea",
        " Ups no deberia haber ido SOLOSOLA ......",
    };

    private void Start()
    {
        dialogueText = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        gender = GameObject.Find("Gender").GetComponent<Gender>();
        StartCoroutine(FadeText(espera));
    }

    IEnumerator FadeText(float delay)
    {
        while (_index < text.Count)
        {
            yield return new WaitForSeconds(delay);
            // 1. Asignar texto
            string procesado = ProcesarGenero(text[_index]);
            dialogueText.text = procesado;
            // 2. Fade in
            yield return FadeAlphaRoutine(1f, 1f);
            // 3. Tiempo visible
            yield return new WaitForSeconds(delay);
            // 4. Fade out
            yield return FadeAlphaRoutine(0f, 1f);
            _index++;
        }
    }

    private IEnumerator FadeAlphaRoutine(float targetAlpha, float duration)
    {
        Color c = dialogueText.color;
        float startAlpha = c.a;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            dialogueText.color = c;
            yield return null;
        }
        c.a = targetAlpha;
        dialogueText.color = c;
    }

    private string ProcesarGenero(string raw)
    {
        string pronombre = gender.GetPronoum();
        string nombre = PlayerData.Instance.PlayerName;

        bool esHombre = PlayerData.Instance.sex == Sex.Boy;
        string objetivo = esHombre ? "minerales" : "hierbas medicinales";
        string elElla = esHombre ? "El" : "Ella";
        string amigo = esHombre ? "amigo" : "amiga";
        string hijo = esHombre ? "hijo" : "hija";
        string profesion = esHombre ? "mineria" : "herboristeria";
        string soloSola = esHombre ? "solo" : "sola";

        // Reemplazos
        raw = raw.Replace("PRONOMBRE",pronombre);
        raw = raw.Replace("PLAYERNAME", nombre);
        raw = raw.Replace("OBJETIVO", objetivo);
        raw = raw.Replace("ELELLA", elElla);
        raw = raw.Replace("AMIGO", amigo);
        raw = raw.Replace("HIJO", hijo);
        raw = raw.Replace("PROFESION", profesion);
        raw = raw.Replace("SOLOSOLA", soloSola);

        return raw;
    }
}
