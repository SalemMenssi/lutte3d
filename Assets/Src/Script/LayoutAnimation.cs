using DG.Tweening;
using UnityEngine;


public class LayoutAnimation : MonoBehaviour
{
    private RectTransform m_RectTransform;


    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }
    public void OpenLayout()
    {
        m_RectTransform.DOScale(Vector3.one, 0.5f);

    }
    public void CloseLayout()
    {
        m_RectTransform.DOScale(Vector3.zero, 0.2f);
    }
}
