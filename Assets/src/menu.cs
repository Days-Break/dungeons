using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
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
            Debug.Log("菜单响应");
            if (visible)
            {
                Show();
            }
            if (!visible)
            {
                Hide();
            }
        }

    }
    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
