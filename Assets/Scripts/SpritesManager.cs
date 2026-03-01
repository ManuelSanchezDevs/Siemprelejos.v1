using UnityEngine;
using UnityEngine.UI;

public class SpritesManager : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public Sprite boy;
    public Sprite girl;

    private void Start()
    {
        ShowSprite();
    }
    private void Update()
    {
        ShowSprite();
    }

    public void ShowSprite()
    {
        if (PlayerData.Instance.sex == Sex.Boy)
        {
            playerSprite.sprite = boy;
        }
        if (PlayerData.Instance.sex == Sex.Girl)
        {
            playerSprite.sprite = girl;
        }
    }

}
