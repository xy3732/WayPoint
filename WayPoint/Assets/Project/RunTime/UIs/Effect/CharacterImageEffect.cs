using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

public class CharacterImageEffect : MonoBehaviour
{
    private Image image { get; set; }

    [field: SerializeField] public GameObject haloObject { get; set; }
    [field: Space(20)]
    [field: SerializeField] private Sprite idle { get; set; }
    [field: SerializeField] private Sprite blink { get; set; }

    [field: SerializeField] private Sprite smile { get; set; }
    [field: SerializeField] private Sprite angry { get; set; }
    [field: SerializeField] private Sprite embarrassment { get; set; }
    [field: SerializeField] private Sprite speak { get; set; }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.sprite = idle;
        transform.DOScale(new Vector3(1, 1, 1), 2f).OnComplete(() => Idle());
    }

    public void Idle()
    {
        doIdle();
    }

    public void Speak()
    {
        doSpeak();
    }

    public void Smile()
    {
        doSmile();
    }

    public void Angry()
    {
        doAngry();
    }


    public void Embarrassment()
    {
        doEmbarrass();
    }

    #region idle
    private void doIdle()
    {
        image.sprite = idle;
        float random = Random.Range(6f, 8f);

        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), random).OnComplete(() => doBlink());
    }

    private void doBlink()
    {
        image.sprite = blink;
        float random = Random.Range(0.15f, 0.25f);

        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), random).OnComplete(() => doIdle());
    }
    #endregion

    #region speak
    private void doSpeak()
    {
        image.sprite = speak;

        transform.DOKill();
        transform.DOScale(new Vector3(1,1,1),0f);
    }
    #endregion

    #region smile
    private void doSmile()
    {
        image.sprite = smile;

        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1),0f);
    }

    #endregion

    #region angry

    private void doAngry()
    {
        image.sprite = angry;

        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1),0f);
    }

    #endregion

    #region embarrassment

    private void doEmbarrass()
    {
        image.sprite = embarrassment;

        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1),0f);
    }

    #endregion

    private void OnDisable()
    {
        transform.DOKill();
    }
}
