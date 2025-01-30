using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationButtons : MonoBehaviour
{
    private RectTransform transform;
    private Vector3 endScale = new Vector3(1.1f, 1.1f, 1.1f);

    private void Start()
    {
        transform = GetComponent<RectTransform>();
        
    }

    public void MouseEnter()
    {
        transform.DOScale(endScale, 0.5f);
    }
    public void MouseExit()
    {
        transform.DOScale(Vector3.one, 0.5f);
    }
}
