using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // �ӵ���prefab
    public GameObject prefabBullet;
    // ����������CDʱ�䳤��
    public float pistolFireCD = 0.2f;
    public float shotgunFireCD = 0.25f;
    public float rifleCD = 0.1f;
    // �ϴο���ʱ��
    float lastFireTime;
    // ��ǰʹ����������
    public int curGun { get; private set; }// 0 ��ǹ��1 ����ǹ��2 �Զ���ǹ
    // ���������ɽ�ɫ�ű�����
    // keyDown������һ֡���¿������keyPressed����������ڳ�������
    // ����������Ϊ��ʵ����ǹ���Զ���ǹ�Ĳ�ͬ�ָ�
    public void Fire(bool keyDown, bool keyPressed)
    {
        // ���ݵ�ǰ���������ö�Ӧ�Ŀ�����
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
    // ��������
    public int Change()
    {
        curGun += 1;
        if(curGun==3)
        {
            curGun = 0;
        }
        return curGun;
    }
    // ��ǹ���ר�ú���
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
    // �Զ���ǹ���ר�ú���
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
    // ����ǹ���ר�ú���
    public void shotgunFire()
    {
        if(lastFireTime+shotgunFireCD>Time.time)
        {
            return;
        }
        lastFireTime = Time.time;
        // ����5���ӵ����໥���10�ȣ��ֲ���ǰ����������
        for(int i=-2;i<=2;i++)
        {
            GameObject bullet = Instantiate(prefabBullet, null);
            Vector3 dir = Quaternion.Euler(0, i * 10, 0) * transform.forward;
            bullet.transform.position = transform.position + dir * 1.0f;
            bullet.transform.forward = dir;
            //// ����ǹ���ӵ��������̣ܶ�����޸��ӵ�����������
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
