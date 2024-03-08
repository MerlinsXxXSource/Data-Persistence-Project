// 2024-03-08 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonLogic : MonoBehaviour
{
    public InputField playerNameInput;
    public void OnPlayButtonPress()
    {
        // Save the player name
        PlayerPrefs.SetString("PlayerName", playerNameInput.text);
        // Initialize the high score for the player
        if (!PlayerPrefs.HasKey(playerNameInput.text))
        {
            PlayerPrefs.SetInt(playerNameInput.text, 0);
        }
        PlayerPrefs.Save();
        // Load the main scene
        SceneManager.LoadScene("main");
    }

    public void OnQuitButtonPress()
    {
        // Quit the game
        Application.Quit();
        
    }

}
