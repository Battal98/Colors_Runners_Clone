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
     

        public SetTurretTargetCommand(ref List<TurretController> turretControllers)
        {
            _turretControllers = turretControllers;
      
        }

        public void Execute(Transform target)
        {
         
            for (var i = 0; i < _turretControllers.Count; i++)
            {
                 

            }
        }
    }

}