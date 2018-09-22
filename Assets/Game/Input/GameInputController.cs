using Game.Player;
using Game.Utilities;

namespace Game.Input
{
    public class GameInputController : SingletonMonoBehaviour<GameInputController>
    {
        private readonly IPlayer[] _player = new IPlayer[4];
		
        private void Update()
        {
            PlayerInput();
        }

        public void SetPlayer(EnumPlayer enumPlayer, IPlayer player)
        {
            _player[(int)enumPlayer] = player;
        }

        private void PlayerInput()
        {
            for (var i = 1; i <= 4; i++)
            {
                NewPlayerJoinGame(i);
                    
                if (_player[i]  == null)
                {
                    return;
                }
				
                var pVertical = UnityEngine.Input.GetAxisRaw("P" +i+"Vertical");
                _player[i].MoveVertical(pVertical);
				
                var pHorizontal = UnityEngine.Input.GetAxisRaw("P" +i+"Horizontal");
                _player[i].MoveHorizontal(pHorizontal);
				
			
                if (UnityEngine.Input.GetButtonDown("P" +i+"UseItem"))
                {
                    _player[i].ThrowItem();
                }

                if (UnityEngine.Input.GetButtonDown("P" +i+"Jump"))
                {
                    _player[i].Jump();
                }

            }
			
        }

        private void NewPlayerJoinGame(int playerId)
        {
            //if player already exists, do not let player join
            if (_player[playerId]  != null)
            {
                return;
            }
            
            if (UnityEngine.Input.GetButtonDown("P" +playerId+"Submit"))
            {
               
            }
        }
    }
}