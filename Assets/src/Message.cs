using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Message : MonoBehaviour
{
    public enum MESSAGE_TYPE { MESSAGE_PLAYER_RUN_AT_TARGET, MESSAGE_PLAYER_BE_HURT, MESSAGE_ENEMY_BE_HURT, MESSAGE_ENEMY_REDUCE_HP, };
    public Player _player;
    public Enemy _enemy;
    public GameObject enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SendGameMessage<T>(MESSAGE_TYPE type, T t)
    {
        switch (type)
        {
            case MESSAGE_TYPE.MESSAGE_PLAYER_RUN_AT_TARGET:
                _player.SendMessage("RunAtTarget", t);
                break;
            case MESSAGE_TYPE.MESSAGE_PLAYER_BE_HURT:
                _player.SendMessage("ReduceHp", t);
                break;
            case MESSAGE_TYPE.MESSAGE_ENEMY_BE_HURT:
                if (_enemy)
                    _enemy.SendMessage("BeHurt");
                break;
            case MESSAGE_TYPE.MESSAGE_ENEMY_REDUCE_HP:
                if (_enemy)
                    _enemy.SendMessage("ReduceHp", t);
                break;
        }
    }
}
