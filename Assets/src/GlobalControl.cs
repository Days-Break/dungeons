using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ClearSky;
public class GlobalControl : MonoBehaviour
{
    void init()
    {
        level = 1;
        exp = 0;
        expMax = 20;
        HP = 75;
        HPMax = 75;
        ATK = 10;
        DEF = 5;
        ID = "";
        alive = true;
    }
    public static GlobalControl Instance;

    //要保存使用的数据;
    public int level;
    public int exp;
    public int expMax;
    public int HP;
    public int HPMax;
    public double ATK;
    public double DEF;
    public string ID;
    public bool alive;
    //初始化
    private void Awake()
    {
        if (Instance == null)
        {
            init();
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    public void LoadData()
    {
        Archive archive = new Archive();
        archive.ReloadData();
        int i = archive.m_DataList._dataList.Count - 1;
        PlayerData data = archive.m_DataList._dataList[i];
        HP = data.HP;
        HPMax = data.HPMax;
        exp = data.exp;
        expMax = data.expMax;
        ATK = data.ATK;
        ID = data.ID;
        DEF = data.DEF;
        level = data.level;
        alive = data.alive;
        Debug.Log("载入成功");
    }
}
