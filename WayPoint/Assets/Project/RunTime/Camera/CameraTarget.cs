using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float threshold;
    [SerializeField] private float centerAncor = 0.5f;

    private void FixedUpdate()
    {
        Transform player = Player.instance.transform;
        Vector3 mousePos = GameManager.instance.mousePos;

        mousePos.x = Mathf.Clamp(mousePos.x, -threshold + player.position.x, threshold + player.position.x);
        mousePos.y = Mathf.Clamp(mousePos.y, -threshold + player.position.y + centerAncor, threshold + player.position.y+ centerAncor);

        transform.position = Vector3.Lerp(transform.localPosition, mousePos, Time.deltaTime * 3);
    }
}
