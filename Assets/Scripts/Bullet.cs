using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 子弹飞行速度
    public float speed = 20.0f;
    // 子弹生命期（几秒之后消失）
    public float lifeTime = 2;
    // 子弹“出生”的时间
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 子弹移动
        transform.position += speed * transform.forward * Time.deltaTime;
        // 超过一段时间销毁自身
        if(startTime+lifeTime<Time.time)
        {
            Destroy(gameObject);
        }
    }
    // 当子弹碰到其他物体时触发
    private void OnTriggerEnter(Collider other)
    {
        // 如果子弹的Tag与被碰撞的物体的Tag相同，则不算打中
        // 如果为了防止同类子弹互相碰撞抵消
        if(CompareTag(other.tag))
        {
            return;
        }
        Destroy(gameObject);
    }
}
