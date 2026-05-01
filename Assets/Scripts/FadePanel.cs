using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public Image panelImage;
    [SerializeField] private float speed = 0.5f;

    private void Awake()
    {
        panelImage = GetComponent<Image>();
    }

    void Start()
    {
        FadeInOut();
    }

    public void FadeInOut()
    {
        float target = panelImage.color.a == 1f ? 0f : 1f;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(target));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        Color c = panelImage.color;
        while (!Mathf.Approximately(c.a, targetAlpha))
        {
            c.a = Mathf.MoveTowards(c.a, targetAlpha, speed * Time.deltaTime);
            panelImage.color = c;
            yield return null;
        }
    }
}
