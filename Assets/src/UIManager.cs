
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    //静音设置
    public GameObject music;
    public Text musicstatus;
    private bool musicflag = true;

    //音量控制
    public AudioSource volume;
    public Scrollbar volumebar;

    //帮助按钮
    public GameObject help;
    private bool helpflag;

    //显示Bar:血量、子弹、时间、敌人
    public Image healthBar;
    public Text bulletCountText;
    public Text TimeBar;
    public Text EnemyLeftBar;

    private void Awake()
    {
        instance = this;
    }

    //更新血条
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
    //更新子弹
    public void UpdateBulletCount(int curAmount, int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + "/" + maxAmount.ToString();
    }

    //刷新时间
    public void UpdateTimeBar(int curtime)
    {
        int min = curtime / 60;
        int sec = curtime % 60;
        if (sec < 10 && min < 10)
        {
            TimeBar.text = "0" + min.ToString() + ":0" + sec.ToString();
        }
        if (sec > 10 && min < 10)
        {
            TimeBar.text = "0" + min.ToString() + ":" + sec.ToString();
        }
    }

    //更新敌人数
    public void UpdateEnemyLeft(int enemyleft)
    {
        EnemyLeftBar.text = enemyleft.ToString();
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    //静音
    public void MusicButton()
    {
        if (musicflag == false)
        {
            volume.volume = 1;
            //music.SetActive(true);
            musicflag = true;
            musicstatus.text = "音乐：开";
            return;
        }

        if (musicflag == true)
        {
            volume.volume = 0;
            //music.SetActive(false);
            musicflag = false;
            musicstatus.text = "音乐：关";
            return;
        }
    }

    //重新开始游戏
    public void ReStart()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    //返回菜单
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    //调节音量
    public void AdjustVolume(Scrollbar volumebar)
    {
        volume.volume = volumebar.value;
    }

    //弹出帮助
    public void helpButton()
    {
        if (helpflag == false)
        {
            help.SetActive(true);
            helpflag = true;
            return;
        }

        if (helpflag == true)
        {
            help.SetActive(false);
            helpflag = false;
            return;
        }
    }
}