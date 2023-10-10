using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class SpeechBuble : MonoBehaviour
{
    public enum SpeechTypes
    { 
        None,
        Reload
    }

    [field: Space(20)]
    [field :SerializeField] private GameObject reloadUI { get; set; }
    [field: SerializeField] private GameObject[] bulletUI { get; set; }

    [field: Space(20)]

    private Vector3 anchor = new Vector3(-0.7f, 1.6f, 0);
    private GameObject target { get; set; }
    
    public SpeechTypes speechType { get; set; }
    public bool stopAnimation { get; set; }

    private void Awake()
    {
        stopAnimation = false;
    }

    public void Update()
    {
        switch (speechType)
        {
            case SpeechTypes.Reload:
                this.transform.position = target.transform.position + anchor;
                reloadUI.SetActive(true);
                break;
        }
    }
   

    public void reloadAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            bulletUI[i].transform.DOScale(new Vector3(0, 0, 0), 0);
            bulletUI[i].transform.DOScale(new Vector3(1, 1, 1), i * 0.3f).SetEase(Ease.OutElastic);
        }

        if(!stopAnimation) gameObject.transform.DOScale(new Vector3(1, 1, 1), 2f).OnComplete( () => repeatReloadAnimation());
    }

    private void repeatReloadAnimation()
    {
        for(int i=0; i<3; i++)
        {
            bulletUI[i].transform.DOScale(new Vector3(0, 0, 0),0);
        }

        gameObject.transform.DOScale(new Vector3(1,1,1),0.25f).OnComplete( () => reloadAnimation());
    }

    public void onSpeech(SpeechTypes type, GameObject target)
    {
        this.target = target;
        speechType = type;
    }

    public void endSpeech()
    {
        this.target = null;

        speechType = SpeechTypes.None;
    }
}
