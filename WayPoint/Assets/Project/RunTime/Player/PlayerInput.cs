using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector3> move { get; set; }
    public Action idle { get; set; }
    public Action shot { get; set; }
    public Action reload { get; set; }

    [HideInInspector] public bool onClickLeft { get; set; }

    private void LateUpdate()
    {
        // �̵� ���� Ű
        GetMovementKeyInput();

        // ���콺 ����
        GetLeftMouseButton();

        // Ű���� R
        GetReloadWeapon();
    }

    // ĳ���� �̵� ó�� 
    private void GetMovementKeyInput()
    {
        if (Input.GetKey(KeyCode.A)) move?.Invoke(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) move?.Invoke(Vector3.right);

        // W, S�� ���� ���߿� ó��
        else if (Input.GetKey(KeyCode.W)) move?.Invoke(Vector3.up);
        else if (Input.GetKey(KeyCode.S)) move?.Invoke(Vector3.down);

        // �׷��� ������ IDLE�� ����
        else idle?.Invoke();
    }

    // ������Ű �ߵ���.
    private void GetReloadWeapon()
    {
        if (Input.GetKey(KeyCode.R)) reload?.Invoke();
    }

    // ���콺 ���� Ŭ����.
    private void GetLeftMouseButton()
    {
         if(Input.GetMouseButton(0)) shot?.Invoke();
    }
}
