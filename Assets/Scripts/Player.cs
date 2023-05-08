using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �ƶ��ٶ�
    public float speed = 3;
    // ���Ѫ��
    public float maxHp = 20;
    // ���������뷽����
    Vector3 input;
    // �Ƿ�����
    bool dead = false;
    // ��ǰѪ��
    float hp;
    Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        // ��ʼȷ����Ѫ״̬
        hp = maxHp;
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // �����̵ĺ����������룬������input������
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Debug.Log(input);
        bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
        bool fireKeyPressed = Input.GetKey(KeyCode.J);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

        // δ������ִ���ƶ��߼�
        if(!dead)
        {
            Move();
            weapon.Fire(fireKeyDown, fireKeyPressed);
            if(changeWeapon)
            {
                ChangeWeapon();
            }
        }
    }
    private void ChangeWeapon()
    {
        int w = weapon.Change();
    }
    void Move()
    {
        // �ȹ�һ�������������������ֱ�ӣ�ͬʱ����б���ƶ�ʱ�ٶȳ��������ٶ�
        input = input.normalized;
        transform.position += input * speed * Time.deltaTime;
        // ���ɫǰ�����ƶ�����һ��
        if (input.magnitude>0.1f)
        {
            transform.forward = input;
        }
        // �����ƶ���ʽû�п����赲�����ʹ������Ĵ��������ƶ���Χ
        Vector3 temp = transform.position;
        const float BORDER = 20;
        if (temp.z > BORDER) { temp.z = BORDER; }
        if (temp.z < -BORDER) { temp.z = -BORDER; }
        if (temp.x > BORDER) { temp.x = BORDER; }
        if (temp.x < -BORDER) { temp.x = -BORDER; }
        transform.position = temp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            if (hp <= 0) { return; }
            hp--;
            if (hp <= 0) { dead = true; }
        }
    }
}
