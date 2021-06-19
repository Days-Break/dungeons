using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using ClearSky;
using LitJson;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataList
{
    public List<PlayerData> _dataList = new List<PlayerData>();
}
public class Archive : MonoBehaviour
{
    public DataList m_DataList = null;
    public PlayerController pc;
    // Use this for initialization
    void Start()
    {
        ReloadData();
        DisplayDataList(m_DataList);
    }
    public void SaveData()
    {
        PlayerData data = pc.d;
        string filePath = Application.dataPath + @"/Resources/Json.txt";
        if (!File.Exists(filePath))
        {
            m_DataList = new DataList();
            m_DataList._dataList.Add(data);
        }
        else
        {
            bool bFind = false;
            foreach (PlayerData saveData in m_DataList._dataList)
            {
                if (data.ID == saveData.ID)
                {
                    saveData.level = data.level;
                    saveData.exp = data.exp;
                    saveData.expMax = data.expMax;
                    saveData.HP = data.HP;
                    saveData.HPMax = data.HPMax;
                    saveData.ATK = data.ATK;
                    saveData.DEF = data.DEF;
                    saveData.alive = data.alive;
                    saveData.attackrange = data.attackrange;
                    bFind = true;
                    Debug.Log("修改成功");
                    break;
                }
            }
            if (!bFind)
                m_DataList._dataList.Add(data);
        }

        FileInfo file = new FileInfo(filePath);
        StreamWriter sw = file.CreateText();
        string json = JsonMapper.ToJson(m_DataList);
        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public bool ReloadData()
    {
        UnityEngine.TextAsset s = Resources.Load("Json") as TextAsset;
        if (!s)
        {
            Debug.Log("读取失败");
            return false;
        }
        string tmp = s.text;
        m_DataList = JsonMapper.ToObject<DataList>(tmp);
        Debug.Log("读取成功");
        return true;
    }

    public void DisplayDataList(DataList dataList)
    {
        if (dataList == null) return;

        foreach (PlayerData info in dataList._dataList)
        {
            Debug.Log("level:" + info.level);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
