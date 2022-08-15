using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Managers;
using Enums;
using DG.Tweening;

public class ColorCheckPhysicController : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    private int _count;

    [SerializeField]
    private ColorCheckAreaManager colorCheckAreaManager;

    [SerializeField]
    private Transform _colHolder;
    public List<GameObject> stackList;


    private void Awake()
    {
        _meshRenderer = this.transform.parent.GetComponentInChildren<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            switch (colorCheckAreaManager.areaType)
            {
                case ColorCheckAreaType.Drone:

                    CoreGameSignals.Instance.onPlayerChangeForwardSpeed?.Invoke(0);
                    StackSignals.Instance.onTransportInStack?.Invoke(other.transform.parent.gameObject, _colHolder);
                    stackList.Add(other.transform.parent.gameObject);
                    other.gameObject.GetComponent<Collider>().enabled = false;
                    MoveCollectables(other.transform.parent.gameObject);
                    break;

                case ColorCheckAreaType.Turret:
                    //forward speed down
                    //change animation state 
                    break;
            }
        }
    }
    public void CheckColor()
    {
        int count = stackList.Count;
        for (int i = 0; i < count; i++)
        {
            var color = _colHolder.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            Debug.Log("character: " + ColorUtility.ToHtmlStringRGB(color) + "GroundMesh: " + ColorUtility.ToHtmlStringRGB(_meshRenderer.material.color));
            if (ColorUtility.ToHtmlStringRGB(_meshRenderer.material.color) != ColorUtility.ToHtmlStringRGB(color))
            {
                Debug.Log(this.name + ": Dead");
                _colHolder.GetChild(0).gameObject.GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Dead);
                stackList.Remove(_colHolder.GetChild(0).gameObject);
                _colHolder.GetChild(0).transform.parent = null;
                stackList.TrimExcess();
            }
            else
            {
                Debug.Log(this.name + ": Alive");
                StackSignals.Instance.onGetStackList?.Invoke(_colHolder.GetChild(0).gameObject);
                stackList.Remove(_colHolder.GetChild(0).gameObject);
                stackList.TrimExcess();
            }
        }
    }

    #region Collectable Movement Color Check Area
    private void MoveCollectables(GameObject other)
    {
        var collectableManager = other.GetComponent<CollectableManager>();
        var randomValue = Random.Range(-1f, 1f);
        other.transform.DOMove(new Vector3(_colHolder.transform.position.x,
            other.transform.position.y,
            _colHolder.transform.position.z + randomValue), 1f).OnComplete(() =>
            {
                collectableManager.SetAnim(CollectableAnimationStates.Crouch);

            });
        other.transform.DORotate(Vector3.zero, 0.1f);
    }
    #endregion


}
