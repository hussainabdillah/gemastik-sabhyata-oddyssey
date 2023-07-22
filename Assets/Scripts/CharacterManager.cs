using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    private int selectedOption = 0;

    public void SelectMale()
    {
        selectedOption = 0;
        Save();
        ChangeScene("NameCharacter");
    }

    public void SelectFemale()
    {
        selectedOption = 1;
        Save();
        ChangeScene("NameCharacter");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
