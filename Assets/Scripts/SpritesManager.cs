using UnityEngine;
using UnityEngine.UI;

public class SpritesManager : MonoBehaviour
{
    public GameObject playerBoy;
    public GameObject playerGirl;

    private void Start()
    {
        PlayerData.Instance.sex = Sex.Girl;
        if (PlayerData.Instance.sex == Sex.Boy)
        {
            Instantiate(playerBoy,transform);
        }
        else
            Instantiate(playerGirl,transform);
    }
}
