using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 子弹的prefab
    public GameObject prefabBullet;
    // 几种武器的CD时间长度
    public float pistolFireCD = 0.2f;
    public float shotgunFireCD = 0.25f;
    public float rifleCD = 0.1f;
    // 上次开火时间
    float lastFireTime;
    // 当前使用哪种武器
    public int curGun { get; private set; }// 0 手枪，1 霰弹枪，2 自动步枪
    // 开火函数，由角色脚本调用
    // keyDown代表这一帧按下开火键，keyPressed代表开火键正在持续按下
    // 这样区分是为了实现手枪和自动步枪的不同手感
    public void Fire(bool keyDown, bool keyPressed)
    {
        // 根据当前武器，调用对应的开火函数
        switch(curGun)
        {
            case 0:
                if(keyDown)
                {
                    pistolFire();
                }
                break;
            case 1:
                if(keyDown)
                {
                    shotgunFire();
                }
                break;
            case 2:
                if(keyPressed)
                {
                    rifleFire();
                }
                break;
        }
    }
    // 更换武器
    public int Change()
    {
        curGun += 1;
        if(curGun==3)
        {
            curGun = 0;
        }
        return curGun;
    }
    // 手枪射击专用函数
    public void pistolFire()
    {
        if(lastFireTime+pistolFireCD>Time.time)
        {
            return;
        }
        lastFireTime = Time.time;
        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
    }
    // 自动步枪射击专用函数
    public void rifleFire()
    {
        if (lastFireTime+rifleCD>Time.time)
        {
            return;
        }
        lastFireTime = Time.time;
        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
    }
    // 霰弹枪射击专用函数
    public void shotgunFire()
    {
        if(lastFireTime+shotgunFireCD>Time.time)
        {
            return;
        }
        lastFireTime = Time.time;
        // 创建5颗子弹，相互间隔10度，分布于前方扇形区域
        for(int i=-2;i<=2;i++)
        {
            GameObject bullet = Instantiate(prefabBullet, null);
            Vector3 dir = Quaternion.Euler(0, i * 10, 0) * transform.forward;
            bullet.transform.position = transform.position + dir * 1.0f;
            bullet.transform.forward = dir;
            //// 霰弹枪的子弹射击距离很短，因此修改子弹的生命周期
            //Bullet b = bullet.GetComponent<Bullet>();
            //b.lifeTime = 0.3f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
