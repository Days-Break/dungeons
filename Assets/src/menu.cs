using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class menu : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "keybd_event")]

    public static extern void Keybd_event(

       byte bvk,//虚拟键值 ESC键对应的是27

       byte bScan,//0

       int dwFlags,//0为按下，1按住，2释放

       int dwExtraInfo//0

       );


    GameObject gameObject;
    public bool visible = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("Menu");
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {

            visible = !visible;
            if (visible)
            {
                Show();
                Time.timeScale = 0;
            }
            if (!visible)
            {
                Hide();
                Time.timeScale = 1.0f;
            }
        }


    }
    public void Show()
    {
        gameObject.SetActive(true);
        Debug.Log("菜单显示");
    }

    public void Hide()
    {
        Debug.Log("菜单隐藏");
        gameObject.SetActive(false);
    }
    public void GoOn()
    {
        Keybd_event(27, 0, 0, 0);
        Keybd_event(27, 0, 2, 0);
    }
}
