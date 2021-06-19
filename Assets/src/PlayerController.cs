using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using ClearSky;
namespace ClearSky
{
    public class PlayerData
    {
        public string ID;
        public int level;
        public int exp;
        public int expMax;
        public double attackrange;
        public int HP;
        public int HPMax;
        public double ATK;
        public double DEF;
        public bool alive;

    }
    public class PlayerController : MonoBehaviour
    {
        public delegate void PlayerListener();
        public event PlayerListener PlayerAttack;
        public Enemy enemy;
        private float movePower = 10f;
        private float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
        public string ID;
        public PlayerData d;
        public int level;
        public int exp;
        public int expMax;
        public double attackrange;
        public int HP;
        public int HPMax;
        public double ATK;
        public double DEF;
        private Rigidbody2D rb;
        private GameObject Over;
        private Animator anim;
        private Transform Cntpos;
        private Hash hash;
        Vector3 movement;
        private float direction = 1;
        //bool isJumping = false;
        public bool alive = true;
        private Slider hslider;
        private Text hptext;
        private Slider eslider;
        private Text eptext;
        private Text lvtext;


        // Start is called before the first frame update
        void Start()
        {
            enemy.EnemyAttack += Hurt;
            enemy.EnemyDied += AddEXP;
            hslider = GameObject.Find("HP").transform.GetComponent<Slider>();
            hptext = GameObject.Find("HP").transform.GetChild(2).GetComponent<Text>();
            eslider = GameObject.Find("EXP").transform.GetComponent<Slider>();
            eptext = GameObject.Find("EXP").transform.GetChild(2).GetComponent<Text>();
            lvtext = GameObject.Find("LV").transform.GetComponent<Text>();
            d.attackrange = attackrange;
            UpdateHP(); UpdateEXP(); UpdateLV();
        }
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Over = GameObject.Find("Over");
            Over.SetActive(false);
            if (LoadData() == false) init();
            if (ID == "")
            {
                hash = new Hash();
                ID = hash.GetHash();
            }
            d = new PlayerData();
        }
        private void Update()
        {
            Restart();
            if (alive)
            {
                // Die();
                // Hurt();
                //Jump();
                Attack();
                Run();
                Rotate();
                Upgrade();
                SaveData();
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }
        private void init()
        {
            HPMax = 75;
            HP = HPMax;
            level = 1;
            expMax = 20;
            exp = 0;
            ATK = 10;
            DEF = 5;
        }

        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -0.5f;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 0.5f, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 0.5f;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 0.5f, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                direction = 0.5f;
                moveVelocity = Vector3.down;

                transform.localScale = new Vector3(0.5f, direction, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                direction = 0.5f;
                moveVelocity = Vector3.up;

                transform.localScale = new Vector3(0.5f, direction, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetTrigger("attack");
                PlayerAttack();
            }
        }
        public float Damage(float DEF)
        {
            return Random.Range((float)ATK * 0.7f, (float)ATK * 1.3f) - Random.Range(DEF * 0.5f, DEF);
        }
        void Hurt()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(2f, 1f), ForceMode2D.Impulse);
            HP -= (int)enemy.Damage((float)DEF);
            UpdateHP();
            if (HP <= 0)
            {
                Die();
            }
            // }
        }
        void Hurt(float x)
        {
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            if (alive)
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(2f, 1f), ForceMode2D.Impulse);
                HP -= (int)x;
                UpdateHP();
                if (HP <= 0)
                {
                    Die();
                }
            }
            // }
        }
        void Die()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha3))
            // {
            anim.SetTrigger("die");
            alive = false;
            Over.SetActive(true);
            // }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
        void UpdateHP()
        {
            hslider.value = (float)HP / HPMax;
            hptext.text = HP.ToString() + "/" + HPMax.ToString();
        }
        void UpdateEXP()
        {
            eslider.value = (float)exp / expMax;
            eptext.text = exp.ToString() + "/" + expMax.ToString();
        }
        void UpdateLV()
        {
            lvtext.text = level.ToString();
        }
        void AddEXP()
        {
            exp += (int)enemy.Exp;
            UpdateEXP();
        }
        void AddEXP(float x)
        {
            exp += (int)x;
            UpdateEXP();
        }
        void Upgrade()
        {
            if (exp > expMax)
            {
                Debug.Log("升级了");
                level = level + 1;
                Debug.Log(level);
                exp -= expMax;
                expMax = (int)(level * 100 * Mathf.Log(2, level));
                HPMax += 10 * level;
                HP = HPMax;
                ATK += level;
                DEF += 0.5f * level;
                UpdateHP();
                UpdateEXP();
                UpdateLV();
            }
        }

        public void SaveData()
        {
            GlobalControl.Instance.HP = HP;
            GlobalControl.Instance.HPMax = HPMax;
            GlobalControl.Instance.exp = exp;
            GlobalControl.Instance.expMax = expMax;
            GlobalControl.Instance.ATK = ATK;
            GlobalControl.Instance.ID = ID;
            GlobalControl.Instance.DEF = DEF;
            GlobalControl.Instance.alive = alive;
            GlobalControl.Instance.level = level;

            d.HP = HP;
            d.HPMax = HPMax;
            d.exp = exp;
            d.expMax = expMax;
            d.ATK = ATK;
            d.ID = ID;
            d.DEF = DEF;
            d.level = level;
            d.alive = alive;
        }

        public bool LoadData()
        {
            if (GlobalControl.Instance != null)
            {
                HP = GlobalControl.Instance.HP;
                HPMax = GlobalControl.Instance.HPMax;
                exp = GlobalControl.Instance.exp;
                expMax = GlobalControl.Instance.expMax;
                ATK = GlobalControl.Instance.ATK;
                ID = GlobalControl.Instance.ID;
                DEF = GlobalControl.Instance.DEF;
                alive = GlobalControl.Instance.alive;
                level = GlobalControl.Instance.level;
                return true;
            }
            else
            {
                return false;
            }
        }
        void Rotate()
        {
            //StartCoroutine(Jump());
        }
    }
}