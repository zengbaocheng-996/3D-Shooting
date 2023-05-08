using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �ӵ������ٶ�
    public float speed = 20.0f;
    // �ӵ������ڣ�����֮����ʧ��
    public float lifeTime = 2;
    // �ӵ�����������ʱ��
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // �ӵ��ƶ�
        transform.position += speed * transform.forward * Time.deltaTime;
        // ����һ��ʱ����������
        if(startTime+lifeTime<Time.time)
        {
            Destroy(gameObject);
        }
    }
    // ���ӵ�������������ʱ����
    private void OnTriggerEnter(Collider other)
    {
        // ����ӵ���Tag�뱻��ײ�������Tag��ͬ���������
        // ���Ϊ�˷�ֹͬ���ӵ�������ײ����
        if(CompareTag(other.tag))
        {
            return;
        }
        Destroy(gameObject);
    }
}
