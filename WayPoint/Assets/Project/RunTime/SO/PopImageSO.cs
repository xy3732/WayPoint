using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PopUpImageSO", menuName = "SO/popupImage")]
public class PopImageSO : ScriptableObject
{
    [field: SerializeField] public Sprite backgroundEffect { get; set; }
    [field: SerializeField] public Sprite backgroundMask {get; set;}
    [field: SerializeField] public Sprite character48sprite { get; set; }
}
