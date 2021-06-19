using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ClearSky;
public class EnemyController : MonoBehaviour
{
    float CreatTime = 5f; //设计创造敌人的时间
    GameObject enemy; //申请一个敌人的模块
    GameObject _heroPanel;
    public GameObject Tenemy;
    public static float number = 0;
    public float numberMax;
    public float checkpoint;//关卡
    void Start()
    {
    }
    void Awake()
    {
        if (checkpoint == 1)
        {
            numberMax = 8;
        }
        else if (checkpoint == 2)
        {
            numberMax = 15;
        }
        else
        {
            numberMax = 30;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (number < numberMax)
        {
            CreatTime -= Time.deltaTime;    //开始倒计时
            if (CreatTime <= 0)    //如果倒计时为0 的时候
            {
                CreatTime = Random.Range(3, 10);     //随机3到9秒内
                                                     // GameObject obj = (GameObject)Resources.Load("template");    //加载预制体到内存
                enemy = Instantiate<GameObject>(Tenemy);    //实例化敌人
                enemy.transform.position = new Vector2(Random.Range(-4f, 40f), Random.Range(-5f, 3f));    //随机生成敌人的位置
                number++;
            }
        }
    }
}
