using UnityEngine;

public class Gender : MonoBehaviour
{
    public string GetPronoum()
    {
        return PlayerData.Instance.sex == Sex.Boy ? "El" : "La";
    }
}
