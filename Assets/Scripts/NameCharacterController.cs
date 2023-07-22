using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameCharacterManager : MonoBehaviour
{
    public InputField nameInputField;
    public Button enterButton;

    private string characterName;

    private void Start()
    {
        enterButton.onClick.AddListener(EnterGame);
        nameInputField.onEndEdit.AddListener(SetName);
    }

    public void SetName(string name)
    {
        characterName = name;
    }

    public void EnterGame()
    {
        if (!string.IsNullOrEmpty(characterName) && characterName.Length < 10)
        {
            // Save the character name to PlayerPrefs
            PlayerPrefs.SetString("CharacterName", characterName);

            SceneManager.LoadScene("PlayGame");
        }
        else
        {
            Debug.Log("Character name should be up to 10 characters long.");
        }
    }
}
