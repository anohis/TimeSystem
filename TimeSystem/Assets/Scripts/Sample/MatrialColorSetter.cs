using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MatrialColorSetter : MonoBehaviour
{
    private Material _material
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = gameObject.GetComponent<Renderer>();
                _cache = _renderer.material.color;
            }

            return _renderer.material;
        }
    }

    [SerializeField] private Color _color;
    private Renderer _renderer;
    private Color _cache;

    [ContextMenu("Execute")]
    public void Execute()
    {
        _material.color = _color;
    }
    [ContextMenu("Invert")]
    public void Invert()
    {
        _material.color = _cache;
    }
}
