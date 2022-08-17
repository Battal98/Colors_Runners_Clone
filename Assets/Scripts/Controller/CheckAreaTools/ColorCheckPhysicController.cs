using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Signals;
using Managers;
using Enums;
using DG.Tweening;

public class ColorCheckPhysicController : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private int _count;

    [SerializeField] private ColorCheckAreaManager colorCheckAreaManager;

    [SerializeField] private Transform _colHolder;
    public List<GameObject> stackList;


    private void Awake()
    {
        _meshRenderer = this.transform.parent.GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            switch (colorCheckAreaManager.AreaType)
            {
                case ColorCheckAreaType.Drone:

                    InvokeSignalsForDrone(other.transform.parent.gameObject);
                    stackList.Add(other.transform.parent.gameObject);
                    other.gameObject.GetComponent<Collider>().enabled = false;
                    colorCheckAreaManager.MoveCollectablesToArea(other.transform.parent.gameObject, _colHolder);
                    break;

                case ColorCheckAreaType.Turret:

                    colorCheckAreaManager.SetTargetForTurrets();
                    StackSignals.Instance.onSetCollectableAnimState(other.transform.parent.gameObject, CollectableAnimationStates.CrouchWalk);
                    // player forward speed down
                    //change animation state 
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Collectable"))
        {
            if (colorCheckAreaManager.AreaType == ColorCheckAreaType.Turret)
            {
                StackSignals.Instance.onSetCollectableAnimState(other.transform.parent.gameObject, CollectableAnimationStates.Run);
            }
        }

    }

    #region Invoke Signals
    private void InvokeSignalsForDrone(GameObject other)
    {
        CoreGameSignals.Instance.onPlayerChangeForwardSpeed?.Invoke(ColorCheckAreaType.Drone);
        StackSignals.Instance.onTransportInStack?.Invoke(other, _colHolder);
    } 
    #endregion

    //burasi mesh controller'a alinacak 
    public void  CheckColorsForDrone()
    {
        int count = stackList.Count;
        transform.GetComponent<Collider>().enabled=false;

        for (int i = 0; i < count; i++)
        {
            var color = _colHolder.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            if (ColorUtility.ToHtmlStringRGB(_meshRenderer.material.color) != ColorUtility.ToHtmlStringRGB(color))
            {
               
                _colHolder.GetChild(0).gameObject.GetComponent<CollectableManager>()
                    .SetAnim(CollectableAnimationStates.Dead);
                stackList.Remove(_colHolder.GetChild(0).gameObject);
                _colHolder.GetChild(0).transform.parent = null;
                stackList.TrimExcess();
            }
            else
            {
                _colHolder.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
                stackList.Remove(_colHolder.GetChild(0).gameObject);
                StackSignals.Instance.onGetStackList?.Invoke(_colHolder.GetChild(0).gameObject);
                stackList.TrimExcess();
            }
        }
    }

    public void CheckColorForTurrets(TurretController turretControllers, Transform target)
    {
        int count = stackList.Count;
        transform.GetComponent<Collider>().enabled = false;

        for (int i = 0; i < count; i++)
        {
            var color = _colHolder.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            if (ColorUtility.ToHtmlStringRGB(_meshRenderer.material.color) != ColorUtility.ToHtmlStringRGB(color))
            {
                Debug.Log("target");
                turretControllers.targetPlayer = target.transform;
                turretControllers.isTargetPlayer = true;
            }
            else
            {
                turretControllers.targetPlayer = null;
                turretControllers.isTargetPlayer = false;
            }
        }
    }


}