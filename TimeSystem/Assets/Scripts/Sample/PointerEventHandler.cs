using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class PointerEvent : UnityEvent<PointerEventData> { }

public class PointerEventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PointerEvent _onClickHandler;
    [SerializeField] private PointerEvent _onEnterHandler;
    [SerializeField] private PointerEvent _onExitHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickHandler?.Invoke(eventData);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _onEnterHandler?.Invoke(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _onExitHandler?.Invoke(eventData);
    }
}
