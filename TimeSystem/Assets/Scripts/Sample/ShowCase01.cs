using System.Collections;
using System.Collections.Generic;
using TimeSystem.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 展示可分別控制單一物體、群體、全域時間流逝
/// </summary>
public class ShowCase01 : MonoBehaviour
{
    [SerializeField]
    [Range(0.001f, 10)]
    private float _onPointerEnterTimeScale;
    private Dictionary<TimeObjectComponent, float> _timeScaleCache = new Dictionary<TimeObjectComponent, float>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        var selected = eventData.pointerPress.GetComponent<TimeObjectComponent>();
        _timeScaleCache.Add(selected, selected.TimeScale);
        selected.TimeScale = _onPointerEnterTimeScale;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        var selected = eventData.pointerPress.GetComponent<TimeObjectComponent>();
        selected.TimeScale = _timeScaleCache[selected];
        _timeScaleCache.Remove(selected);
    }
}
