using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
public class characterEmotion : MonoBehaviour
{
    private Image image { get; set; }

    [field: SerializeField] private Sprite questionMark { get; set; }
    [field: SerializeField] private Sprite shine { get; set; }
    [field: SerializeField] private Sprite waterDrop { get; set; }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        None();
    }
    
    public void None()
    {
        doNone();
    }

    public void QuestionMark()
    {
        doQuestionMark();
    }

    public void Shine()
    {
        doShine();
    }
  
    public void WaterDrop()
    {
        doWaterDrop();
    }

    #region none
    private void doNone()
    {
        image.color= new Color32(255,255,255,0);
    }
    #endregion

    #region questionMark

    private void doQuestionMark()
    {
        image.sprite = questionMark;
        image.color = new Color32(255, 255, 255, 255);
    }
    #endregion

    #region shine

    private void doShine()
    {
        image.sprite = shine;
        image.color = new Color32(255, 255, 255, 255);
    }
    #endregion

    #region waterDrop
    private void doWaterDrop()
    {
        image.sprite = waterDrop;
        image.color = new Color32(255, 255, 255, 255);
    }
    #endregion

    private void OnDisable()
    {
        transform.DOKill();
    }
}
