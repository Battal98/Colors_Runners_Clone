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
    [SerializeField]
    private List<GameObject> stackList;


    private void Awake()
    {
        _meshRenderer = this.transform.parent.GetComponentInChildren<MeshRenderer>();
    }
    public void CheckColor()
    {
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

    #region Collectable Movement Color Check Area
    private void MoveCollectables(GameObject other)
    {
        var collectableManager = other.GetComponent<CollectableManager>();
        var randomValue = Random.Range(-1f, 1f);
        _count++;
        other.transform.DOMove(new Vector3(_colHolder.transform.position.x,
            other.transform.position.y,
            _colHolder.transform.position.z + randomValue), 1f).OnComplete(() =>
            {
                collectableManager.SetAnim(CollectableAnimationStates.Crouch);

            });
        other.transform.DORotate(Vector3.zero, 0.1f);

        //StartCoroutine(CheckCount());

    }

    private IEnumerator CheckCount()
    {
        if (_count >= stackList.Count)
        {
            #region Collectable Material Outline Jobs

            OutlineJobs(0);

            #endregion

            #region Drone Movement and Color Check Jobs

            CameraSignals.Instance.onSetCameraTarget?.Invoke(null);
            yield return new WaitForSeconds(.5f); // wait for before drone movement 

            colorCheckAreaManager.PlayDroneAnim();

            yield return new WaitForSeconds(7.5f / 2f);// kill wrong collectables
            CheckColor();
            yield return new WaitForSeconds(7.5f / 2f);// wait for drone movement
            //AfterDroneMovementJobs();
            #endregion
        }
    }

    private void OutlineJobs(float endValue)
    {
        for (int i = 0; i < stackList.Count; i++)
        {
            var materialColor = stackList[i].GetComponentInChildren<SkinnedMeshRenderer>().material;
            materialColor.DOFloat(endValue, "_OutlineSize", 0.5f);
        }

    }

    private void AfterDroneMovementJobs(PlayerManager _playerManager)
    {
        StackSignals.Instance.onGetStackList?.Invoke(stackList);
        CameraSignals.Instance.onSetCameraTarget?.Invoke(_playerManager.transform);
        _playerManager.transform.DOMoveZ(_playerManager.transform.position.z + 2.9f, .5f);
        CoreGameSignals.Instance.onPlayerChangeForwardSpeed?.Invoke(1);
        OutlineJobs(15);
    }
    #endregion

}
