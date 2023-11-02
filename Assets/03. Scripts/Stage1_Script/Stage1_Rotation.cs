using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Rotation : MonoBehaviour
{
    public float targetRotation = 30f; // ��ǥ ȸ�� ����
    public float totalRotation = 0f;   // ���� ȸ�� ������ ����
    public float rotationSpeed = 5f;   // ȸ�� �ӵ�

    private Quaternion initialRotation;
    private Quaternion targetQuaternion;
    private bool isRotating = false;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private IEnumerator RotateObject()  // ������Ʈ ȸ�� �Լ�
    {
        isRotating = true;  // ȸ�� ���۽� ȸ�����̶� ���� true�� ��ȯ
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            float t = elapsedTime / 1f;
            transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, t);
            yield return null;
        }

        isRotating = false; // ȸ���� ������ false�� ��ȯ
    }

    public void RotateTerrain()
    {
        if (!isRotating)    // ȸ������ �ƴҶ�
        {
            totalRotation += targetRotation;    // �� ȸ�� ���� += ��ǥ ȸ�� ����
            targetQuaternion = Quaternion.Euler(0, 0, totalRotation);   // �� ȸ�� ������ �̿��Ͽ� ��ǥ ���� ����
            StartCoroutine(RotateObject());     // ������Ʈ ȸ��
        }
    }
}
