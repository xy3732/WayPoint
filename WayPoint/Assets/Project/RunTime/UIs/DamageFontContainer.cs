using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFontContainer : Singleton<DamageFontContainer>
{
    [field: SerializeField] public GameObject DamagePrefab { get; set; }
    public Queue<GameObject> pool = new Queue<GameObject>();


    public void createDamageEffect(GameObject parent, float damage, Color32 color)
    {
        Pooling.instance.getUiObject(ref pool,this.gameObject , parent, DamagePrefab, damage, color);
    }
     
    public void endEffect(GameObject gameobject)
    {
        Pooling.instance.setObject(ref pool, gameobject);
    }    
}
