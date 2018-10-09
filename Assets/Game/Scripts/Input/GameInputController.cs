using System.Collections.Generic;
using Game.Player;
using Game.Utilities;
using UnityEngine;

namespace Game.Input
{
    public class GameInputController : MonoBehaviour
    {
        private IPlayer[] _player = new IPlayer[4];
		
        private void Update()
        {
            PlayerInput();
        }

        public IEnumerable<IPlayer> GetPlayers()
        {
            return _player;
        }
     

        public void SetPlayer(EnumPlayer enumPlayer, IPlayer player)
        {
            _player[(int)enumPlayer] = player;
        }

        private void PlayerInput()
        {
          
            for (var i = 0; i < _player.Length; i++)
            {
                NewPlayerJoinGame(i);
                    
                if (_player[i] == null)
                {
                    continue;
                }
				
                var pHorizontal = UnityEngine.Input.GetAxisRaw("P" +(i+1)+"Horizontal");
                _player[i].MoveHorizontal(pHorizontal);
			
                if (UnityEngine.Input.GetButtonDown("P" +(i+1)+"UseItem"))
                {
                    _player[i].ThrowItem();
                }

                if (UnityEngine.Input.GetButtonDown("P" +(i+1)+"Jump"))
                {
                    _player[i].Jump();
                }

            }
			
        }

        private void NewPlayerJoinGame(int playerId)
        {
            //if player already exists, do not let player join
            if (_player[playerId] != null)
            {
                return;
            }
            
            if (UnityEngine.Input.GetButtonDown("P" +(playerId + 1)+"Submit"))
            {
               
            }
        }
    }
}