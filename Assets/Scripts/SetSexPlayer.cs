using UnityEngine;

public class SetSexPlayer : MonoBehaviour
{
    public GameObject playerBoy;
    public GameObject playerGirl;
    private PlayerController playerController;

    public static GameObject currentPlayer;

    private void Awake()
    {
        if (PlayerData.Instance.sex == Sex.Boy)
        {
            currentPlayer = Instantiate(playerBoy, transform);
        }
        else
            currentPlayer = Instantiate(playerGirl, transform);
    }

    private void Start()
    {
        playerController = currentPlayer.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        else
            print("No se a encontrado a player controller");
    }
}
