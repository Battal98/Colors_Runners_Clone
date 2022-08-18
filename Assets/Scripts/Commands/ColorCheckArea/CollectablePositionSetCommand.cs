using UnityEngine;
using Managers;
using DG.Tweening;
using Enums;

namespace Commands
{
    public class CollectablePositionSetCommand
    {
        public void Execute(GameObject other, Transform _colHolder)
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
    }
}