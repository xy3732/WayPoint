using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

public class WeaponAnchor : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject weaponObject;

    [SerializeField] private Vector3 anchor;

    private void Awake()
    {
        playerObject = Player.instance.gameObject;
        weaponObject = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        AnchorPlayer();
        MouseLookUp();
        flip();
    }

    private void AnchorPlayer()
    {
        transform.position = playerObject.transform.position + anchor;
    }

    private void flip()
    {
        if (transform.rotation.eulerAngles.z > 90 
            && transform.rotation.eulerAngles.z < 270)
        {
            weaponObject.transform.localScale = new Vector3(1, -1, 1);
            Player.instance.FlipSprite(FlipVector3.Left);
        }
        else
        {
            weaponObject.transform.localScale = new Vector3(1, 1, 1);
            Player.instance.FlipSprite(FlipVector3.Right);
        }
    }

    private void MouseLookUp()
    {
        if (UIManager.instance.ScriptsObject.activeSelf) return;

        GameManager gameManager = GameManager.instance;
        var angle = Mathf.Atan2(
            gameManager.mousePos.y - transform.position.y, 
            gameManager.mousePos.x - transform.position.x
            ) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
    }
}

