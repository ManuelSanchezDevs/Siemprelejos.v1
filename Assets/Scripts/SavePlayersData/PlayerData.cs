using UnityEngine;

public enum Sex
{
    Boy,
    Girl
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private string _playerName = "Manolo";
    private string _selectedCharacter;

    public string PlayerName { get { return _playerName; } set { _playerName = value; } }

    public string SelectedCharacter { get { return _selectedCharacter; } set { _selectedCharacter = value; } }
    public Sex sex;

    private void Awake()
    {

        Debug.Log("PlayerData creado en: " + gameObject.scene.name);

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
        SelectedCharacter = characterName;
        this.sex = sex;
    }
}
    
