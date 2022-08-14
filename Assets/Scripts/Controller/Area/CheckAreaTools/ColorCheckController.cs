using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCheckController : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Transform _colHolder;
    private int _count;

    private void Awake()
    {
        _meshRenderer = this.transform.parent.GetComponentInChildren<MeshRenderer>();
    }
    public void CheckColor()
    {
        Debug.Log("this: " + _meshRenderer.material.color);
        for (int i = 0; i < _colHolder.childCount; i++)
        {
            var color = _colHolder.GetChild(i).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            if (_meshRenderer.material.color != color)
            {
                Debug.Log(this.name + ": Dead");
            }
            else
            {
                Debug.Log(this.name + ": Alive");
            }
        }

    }

}
