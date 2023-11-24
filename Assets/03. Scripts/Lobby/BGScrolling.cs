using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGScrolling : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    // ��ũ�ѿ� ���� BG ����
    public float BGCount = 2;

    void Update()
    {
        MoveUpObject();
    }

    void MoveUpObject()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        // ȭ�� ���� ����� �Ʒ��� �̵��Ѵ�.
        if(transform.position.y >= transform.localScale.y)
        {
            transform.position += BGCount * transform.localScale.y * Vector3.down;
        }
    }
}
