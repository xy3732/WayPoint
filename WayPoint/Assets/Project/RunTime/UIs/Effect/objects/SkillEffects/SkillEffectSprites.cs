using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class SkillEffectSprites : MonoBehaviour
{
    [field: SerializeField] private GameObject backgroundMaskObject { get; set; }  
    [field: SerializeField] private GameObject characterSpriteObject { get; set; }
  
    [field: Space(20)]

    [field: SerializeField] public Sprite backgroundEffect { get; set; }
    [field: SerializeField] public Sprite backgroundMask { get; set; }
    [field: SerializeField] public Sprite character { get; set; }



    private SpriteRenderer backGroundEffectSprite { get; set; }
    private SpriteRenderer characterSprite { get; set; }
    private SpriteMask mask { get; set; }
    private Tweener tweener { get; set; }

    private float duration {get ;set;}
    private float animationDuration { get; set; }
    private bool shake { get; set; }

    private void Awake()
    {
        backGroundEffectSprite = GetComponent<SpriteRenderer>();
        characterSprite = characterSpriteObject.GetComponent<SpriteRenderer>();
        mask = backgroundMaskObject.GetComponent<SpriteMask>();
    }

    private void Update()
    {
        duration += Time.deltaTime;
        animationDuration += Time.deltaTime;

        if(animationDuration >0.8f)
        {
            animationDuration = 0;
            shake = !shake;
            doShake(shake);
        }

        if(duration >= 4f)
        {
            onEnd();
        }
    }

    private void OnEnable()
    {
        transform.position = Player.instance.transform.position + new Vector3(2.25f, 3f, 0f);

        duration = 0;
        animationDuration = 0;
        shake = false;

        set();
    }

    private void OnDisable()
    {
        tweener.Kill();
    }

    private void onStart()
    {
        tweener = transform.DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutExpo);
    }

    private void onEnd()
    {
        tweener = transform.DOScale(new Vector3(0,0,0), 0.4f).SetEase(Ease.OutQuad).OnComplete( () => active(false));
    }

    private void active(bool set)
    {
        transform.DOKill();
        gameObject.SetActive(set);
    }

    private void doShake(bool doCheck)
    {
        if(doCheck) tweener = transform.DORotate(new Vector3(0, 0, 10), 0f);
        else tweener = transform.DORotate(new Vector3(0, 0, -10), 0f);
    }

    private void set()
    {
        backGroundEffectSprite.sprite = backgroundEffect;
        characterSprite.sprite = character;
        mask.sprite = backgroundMask;

        tweener = transform.DOScale(new Vector3(0.2f,0.2f,0.2f),0f).OnComplete( () => onStart()).SetAutoKill(false);
    }
}
