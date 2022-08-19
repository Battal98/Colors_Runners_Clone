using Data.ValueObject;
using UnityEngine;
using Keys;


namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Swerve", menuName = "Movement/SwerveMove", order = 0)]
    public class CD_MoveType_Swerve : CD_Movement
    {
        public override void DoMovement(ref float _colorAreaSpeed, ref bool _isReadyToMove, ref Rigidbody _rigidbody,
            ref InputParams _inputParams, ref PlayerMovementData _playerMovementData)
        {
            if (_isReadyToMove)
            {
                SwerveMove(ref _colorAreaSpeed, ref _rigidbody, ref _playerMovementData, ref _inputParams);
            }
            else
            {
                StopSideways(ref _colorAreaSpeed, ref _rigidbody, ref _playerMovementData);
            }
        }
        private void SwerveMove(ref float _colorAreaSpeed, ref Rigidbody _rigidbody,
            ref PlayerMovementData _playerMovementData,
            ref InputParams _inputParams)
        {
            _rigidbody.velocity = new Vector3(
                _inputParams.Values.x * _playerMovementData.SidewaysSpeed,
                _rigidbody.velocity.y,
                _playerMovementData.ForwardSpeed * _colorAreaSpeed);

          
        
            _rigidbody.position = new Vector3(
                 Mathf.Clamp(_rigidbody.position.x, -_inputParams.ClampValues.x, _inputParams.ClampValues.x),
                 _rigidbody.position.y,
                _rigidbody.position.z);
        }

        private void StopSideways(ref float _colorAreaSpeed, ref Rigidbody _rigidbody,
            ref PlayerMovementData _playerMovementData)
        {
            _rigidbody.velocity =
                new Vector3(0, _rigidbody.velocity.y, _playerMovementData.ForwardSpeed * _colorAreaSpeed);
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}