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
        // 이동 관련 키
        GetMovementKeyInput();

        // 마우스 왼쪽
        GetLeftMouseButton();

        // 키보드 R
        GetReloadWeapon();
    }

    // 캐릭터 이동 처리 
    private void GetMovementKeyInput()
    {
        if (Input.GetKey(KeyCode.A)) move?.Invoke(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) move?.Invoke(Vector3.right);

        // W, S는 가장 나중에 처리
        else if (Input.GetKey(KeyCode.W)) move?.Invoke(Vector3.up);
        else if (Input.GetKey(KeyCode.S)) move?.Invoke(Vector3.down);

        // 그렇지 않으면 IDLE로 설정
        else idle?.Invoke();
    }

    // 재장전키 발동시.
    private void GetReloadWeapon()
    {
        if (Input.GetKey(KeyCode.R)) reload?.Invoke();
    }

    // 마우스 왼쪽 클릭시.
    private void GetLeftMouseButton()
    {
         if(Input.GetMouseButton(0)) shot?.Invoke();
    }
}
