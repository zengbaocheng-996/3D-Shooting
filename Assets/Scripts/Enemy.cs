using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 用于制作死亡效果的预制体，暂时不用
    public GameObject prefabBoomEffect;
    public float speed = 2;
    public float fireTime = 0.1f;
    public float maxHp = 1;
    Vector3 input;
    Transform player;
    float hp;
    bool dead = false;
    Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        //根据Tag查找玩家物体
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    void Move()
    {
        // 玩家的input是从键盘输入而来，而敌人的input则是始终指向玩家的方向
        input = player.position - transform.position;
        Debug.Log(player.position);
        input = input.normalized;
        Debug.Log(input);
        transform.position += input * speed * Time.deltaTime;
        if(input.magnitude>0.1f)
        {
            transform.forward = input;
        }
    }
    void Fire()
    {
        // 一直开枪，开枪的频率可以通过武器启动控制
        weapon.Fire(true, true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            hp--;
            if (hp <= 0)
            { 
                dead = true;
                // 这里可以添加死亡特效
                Instantiate(prefabBoomEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
