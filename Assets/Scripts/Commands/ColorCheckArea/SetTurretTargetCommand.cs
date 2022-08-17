using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Controllers;

namespace Commands.ColorCheckArea
{
    public class SetTurretTargetCommand
    {
        private List<TurretController> _turretControllers;
        private List<ColorCheckPhysicController> _colorCheckPhysicController;

        public SetTurretTargetCommand(ref List<TurretController> turretControllers,
            ref List<ColorCheckPhysicController> colorCheckPhysicControllers)
        {
            _turretControllers = turretControllers;
            _colorCheckPhysicController = colorCheckPhysicControllers;
        }

        public void Execute(int index, Transform target)
        {
         
            for (var i = 0; i < _turretControllers.Count; i++)
            {
                _colorCheckPhysicController[index].CheckColorForTurrets(_turretControllers[i], target);

            }
        }
    }

}