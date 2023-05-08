using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ������������Ч����Ԥ���壬��ʱ����
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
        //����Tag�����������
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
        // ��ҵ�input�ǴӼ�����������������˵�input����ʼ��ָ����ҵķ���
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
        // һֱ��ǹ����ǹ��Ƶ�ʿ���ͨ��������������
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
                // ����������������Ч
                Instantiate(prefabBoomEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
