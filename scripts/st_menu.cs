using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class st_menu : MonoBehaviour
{
    [SerializeField] public string DEFAULT_LVL;
    [SerializeField] TMPro.TextMeshProUGUI statsText;

    public UnityEngine.UI.Slider sl;

    public float time;
    public bool ignoreMusic = false;

    #region vibration
    public void SetSlTime()
    {
        time = sl.value;
    }
    public void VibrateTime()
    {
        Vibration.Vibrate(Mathf.RoundToInt(time * 1000));
    }
    public void TapPopVibrate()
    {
        Vibration.VibratePop();
    }

    public void TapPeekVibrate()
    {
        Vibration.VibratePeek();
    }

    public void TapNopeVibrate()
    {
        Vibration.VibrateNope();
    }
    #endregion vibration
    #region main
    public void CopyToClipboard()
    {
        string s = statsText.text;
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
    }
    void Start()
    {
        if (!ignoreMusic)
        {
            PlayerPrefs.SetFloat("last_ad_time", 0f);
            AudioManager.AudioManager.m_instance.PlayMusic("Menu");
        }
        
        if (PlayerPrefs.HasKey("buttonsTransparency") == false)
        {
            PlayerPrefs.SetFloat("buttonsTransparency", 0.5f);
        }
        if (statsText != null)
        {
            string t = "";

            t += "game," + PlayerPrefs.GetInt("_game_deaths").ToString() + "," + PlayerPrefs.GetFloat("_game_time").ToString();
            t += "\n";
            t += "\n";

            for (int i = 0; i < 21; ++i)
            {
                float g = PlayerPrefs.GetFloat("_lvl_time_Lvl" + i.ToString());
                t += "lvl_" + i.ToString() + "," + PlayerPrefs.GetInt("_lvl_deaths_Lvl" + i.ToString()) + ",";
                t += System.Math.Round(g);
                t += "\n";
            }

            statsText.text = t;
        }
    }
    public void ToGame()
    {
        if (PlayerPrefs.HasKey("ContinueLvl"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLvl"));
        }
        else
        {
            SceneManager.LoadScene(DEFAULT_LVL);
        }
    }
    public void Settings()
    {
        SceneManager.LoadScene("Scenes/Settings");
    }
    public void About()
    {
        SceneManager.LoadScene("Scenes/About");
    }
    public void ToMain()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
    public void LoadLvl(string lvl)
    {
        PlayerPrefs.SetInt("CheckpointIndex", 0);
        SceneManager.LoadScene(lvl);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
    public void SetLanguage(int lang)
    {
        PlayerPrefs.SetInt("Language", lang);
        ResetScene();
    }
    public void openURL(string url)
    {
        Application.OpenURL(url);
    }
    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    #endregion main
}
