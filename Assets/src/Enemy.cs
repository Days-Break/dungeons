using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ClearSky;
public class Enemy : MonoBehaviour
{
    public delegate void EnemyListener();
    public event EnemyListener EnemyAttack;
    public event EnemyListener EnemyDied;
    float CreatTime = 5f; //设计创造敌人的时间
    GameObject enemy; //申请一个敌人的模块
    GameObject _heroPanel;
    public PlayerController PC;
    private Rigidbody2D rb;
    private Animator anim;

    public Slider slider;
    public float attackrange;
    public GameObject player;
    public GameObject Tenemy;
    public float movespeed;
    public float attackspeed;
    public static float number = 0;
    public float numberMax;
    public float HP;
    public float HPMax;
    public float ATK;
    public float DEF;
    public float Exp;
    public float checkpoint;//关卡
    void Start()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();

    }
    void Awake()
    {
        if (checkpoint == 1)
        {
            HPMax = Random.Range(10, 20);
            ATK = Random.Range(7, 9);
            DEF = Random.Range(3, 4);
            numberMax = 8;
            attackspeed = 1.2f;
        }
        else if (checkpoint == 2)
        {
            HPMax = Random.Range(40, 60);
            ATK = Random.Range(10, 20);
            DEF = Random.Range(7, 9);
            numberMax = 15;
            attackspeed = 0.8f;
        }
        else
        {
            HPMax = Random.Range(100, 150);
            ATK = Random.Range(30, 50);
            DEF = Random.Range(15, 25);
            numberMax = 30;
            attackspeed = 0.5f;
        }
        HP = HPMax;
        Exp = (int)(HPMax / 2 + ATK * 2 + DEF);
        InvokeRepeating("Attack", 2, attackspeed);
        slider.value = 1;
        PC.PlayerAttack += Hurt;
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
    void FixedUpdate()

    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > attackrange)
        {
            transform.Translate(Vector3.right * movespeed * Time.deltaTime);
        }
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Attack()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= attackrange)
        {
            // EnemyAttack();
            player.SendMessage("Hurt", (int)Damage((float)PC.DEF));
            anim.SetTrigger("attack");
            Debug.Log("敌人发起攻击");
        }
    }
    void Hurt()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < PC.attackrange)
        {
            anim.SetTrigger("hurt");
            HP -= (float)PC.Damage((float)DEF);
            if (HP <= 0)
            {
                Die();
            }
            slider.value = HP / HPMax;
            Debug.Log("敌人受伤了");
            rb.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
        }
    }
    public float Damage(float DEF)
    {
        return Random.Range(ATK * 0.7f, ATK * 1.3f) - Random.Range(DEF * 0.5f, DEF);
    }
    void Die()
    {
        anim.SetTrigger("death");
        Debug.Log("敌人被消灭了");
        PC.PlayerAttack -= Hurt;
        CancelInvoke();
        // EnemyDied();
        player.SendMessage("AddEXP", (int)Exp);
        Debug.Log(Exp);
        Destroy(transform.GetChild(1).gameObject);
        Destroy(transform.GetChild(0).gameObject);
    }
}
