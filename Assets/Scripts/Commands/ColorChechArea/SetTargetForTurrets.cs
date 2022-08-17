using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Controllers;

public class SetTargetForTurretCommand
{
    private List<TurretController> _turretControllers;
    private List<ColorCheckPhysicController> _colorCheckPhysicController;

    public SetTargetForTurretCommand(ref List<TurretController> turretControllers, ref List<ColorCheckPhysicController> colorCheckPhysicControllers)
    {
        _turretControllers = turretControllers;
        _colorCheckPhysicController = colorCheckPhysicControllers;
    }

    public void Execute(int index, Transform target)
    {
        Debug.Log("execute");
        for (var i = 0; i < _turretControllers.Count; i++)
        {
            _colorCheckPhysicController[index].CheckColorForTurrets(_turretControllers[i], target);

        }
    }
}
