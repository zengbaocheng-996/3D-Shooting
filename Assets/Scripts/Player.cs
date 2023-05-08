using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移动速度
    public float speed = 3;
    // 最大血量
    public float maxHp = 20;
    // 变量，输入方向用
    Vector3 input;
    // 是否死亡
    bool dead = false;
    // 当前血量
    float hp;
    Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        // 初始确保满血状态
        hp = maxHp;
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // 将键盘的横向、纵向输入，保存在input变量中
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Debug.Log(input);
        bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
        bool fireKeyPressed = Input.GetKey(KeyCode.J);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

        // 未死亡则执行移动逻辑
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
        // 先归一化输入向量，让输入更直接，同时避免斜向移动时速度超过最大加速度
        input = input.normalized;
        transform.position += input * speed * Time.deltaTime;
        // 令角色前方与移动方向一致
        if (input.magnitude>0.1f)
        {
            transform.forward = input;
        }
        // 以上移动方式没有考虑阻挡，因此使用下面的代码限制移动范围
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
