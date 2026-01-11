using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChargeBarScenes : MonoBehaviour
{
    [SerializeField] private Slider sliderCharge;
    [SerializeField] private float velocity;
    [SerializeField] private float delayTime;

    void Start()
    {
        sliderCharge = GameObject.Find("Slider").GetComponent<Slider>();
        sliderCharge.value = 0;
    }

    void Update()
    {
        ChargeBarra();
    }

    public void ChargeBarra()
    {
        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            return;
        }

        if (sliderCharge.value < 100)
        {
            sliderCharge.value += Time.deltaTime * velocity;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
