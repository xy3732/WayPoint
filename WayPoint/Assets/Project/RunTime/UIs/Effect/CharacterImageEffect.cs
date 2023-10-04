using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

public class CharacterImageEffect : MonoBehaviour
{
    private Image image { get; set; }

    [field: SerializeField] private Sprite idle { get; set; }
    [field: SerializeField] private Sprite blink { get; set; }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.sprite = idle;
        transform.DOScale(new Vector3(1, 1, 1), 2f).OnComplete(() => doBlink());
    }

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

    private void OnDisable()
    {
        transform.DOKill();
    }
}
