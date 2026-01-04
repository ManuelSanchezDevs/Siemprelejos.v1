using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Slider sliderCharge;
    [SerializeField] private float velocity;
    [SerializeField] private float delayTime;

    void Start()
    {
        sliderCharge = GameObject.Find("Slider").GetComponent<Slider>();
        sliderCharge.value = 0;
        
    }

    private void Update()
    {
        ChargeBarra();
    }

    public void ChargeBarra()
    {
        if (delayTime > 0)
        {
            delayTime -= 1 * Time.deltaTime;
        }

        if (sliderCharge.value < 100 && delayTime <= 0)
        {
            sliderCharge.value += 1 * Time.deltaTime * velocity;
        }
        if(sliderCharge.value == 100)
        {
            SceneManager.LoadScene(1);
        }
    }
}
