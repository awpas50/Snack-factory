using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1;
    }
    public void Quit()
    {
        AudioManager.instance.Play(SoundList.ButtonClick);
        Application.Quit();
    }
    public void Restart()
    {
        AudioManager.instance.Play(SoundList.ButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        AudioManager.instance.Play(SoundList.ButtonClick);
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadTutorial()
    {
        AudioManager.instance.Play(SoundList.ButtonClick);
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadMainGame()
    {
        AudioManager.instance.Play(SoundList.ButtonClick);
        SceneManager.LoadScene("MainGame");
    }
}
