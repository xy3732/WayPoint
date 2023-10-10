using UnityEngine;

using DG.Tweening;

public class HaloEffect : MonoBehaviour
{
    [field: SerializeField] private float haloY { get; set; }
    private float sinY { get; set; }
    private float runtime { get; set; }

    private RectTransform rect { get; set; }
    private void OnEnable()
    {
        rect = gameObject.GetComponent<RectTransform>();

        sinY = 0;
        runtime = 0;

    }

    private void FixedUpdate()
    {
        runtime += Time.deltaTime * 1f;

        sinY = 5f * Mathf.Sin(runtime);

        rect.DOKill();
        rect.DOAnchorPosY(haloY + sinY, 0f);
    }

    private void OnDisable()
    {
        rect.DOKill();
    }
}
