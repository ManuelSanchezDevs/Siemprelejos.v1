using UnityEngine;

public enum Sex
{
    Boy,
    Girl
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public string playerName = "Manolo";
    public string selectedCharacter;
    public Sex sex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetCharacter(string characterName, Sex sex)
    {
        selectedCharacter = characterName;
        this.sex = sex;
    }
}
    
