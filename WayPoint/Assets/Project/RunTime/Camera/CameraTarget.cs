using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float threshold;

    private void FixedUpdate()
    {
        Transform player = Player.instance.transform;
        Vector3 mousePos = GameManager.instance.mousePos;

        Vector3 targetPos = (mousePos);

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        transform.position = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 3);
        //transform.position += targetPos;

    }
}
