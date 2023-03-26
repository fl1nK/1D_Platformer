﻿using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntetyInputSource> _inputSources;

        public PlayerBrain(PlayerEntity playerEntity, List<IEntetyInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontal(GetHorizontalDirection());
            _playerEntity.MoveDown(GetVerticalDirection());
            
            if(IsJump)
                _playerEntity.Jump();
            
            if(IsAttack)
                _playerEntity.StartAttack();

            foreach (var inputSource in _inputSources)
            {
                inputSource.ResetOneTimeActions();
            }
        }

        

        private float GetHorizontalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.HorizontalDirection == 0)
                    continue;

                return inputSource.HorizontalDirection;
            }

            return 0;
        }

        private float GetVerticalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.VerticalDirection == 0)
                    continue;

                return inputSource.VerticalDirection;
            }

            return 0;
        }
        private bool IsJump => _inputSources.Any(source => source.Jump);
        private bool IsAttack => _inputSources.Any(source => source.Attack);
    }
}