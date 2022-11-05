using System;
using Joystick_and_Swipe;
using PersonComponent;
using UniRx;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class MovementController : IFixedTickable
    {
        private readonly JoystickController _joystickController;
        private readonly Rigidbody _rigidBody;
        private readonly float _speed;
        private readonly PersonAnimationController _personAnimationController;

        private bool _isMove;
        
        public MovementController(JoystickController joystickController, Player.Settings playerSettings)
        {
            _joystickController = joystickController;
            _rigidBody = playerSettings.Rigidbody;
            _speed = playerSettings.Speed;
            _personAnimationController = new PersonAnimationController(playerSettings.Animator);
        }

        public void FixedTick()
        {
            _rigidBody.velocity = _speed * _joystickController.InputDirection;
            
            var isRun = _joystickController.InputDirection != Vector3.zero || _joystickController.IsDrag;
            _personAnimationController.SetRun(isRun);
            
            var direction = _rigidBody.velocity;
            if (!direction.Equals(Vector3.zero))
            {
                _rigidBody.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}