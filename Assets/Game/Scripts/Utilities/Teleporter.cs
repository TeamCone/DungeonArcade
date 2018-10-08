using UnityEngine;

namespace Game.Scripts.Utilities
{
	public class Teleporter : MonoBehaviour
	{
		[SerializeField] private Transform _teleportTarget;
	
		private void OnCollisionStay2D(Collision2D other)
		{
			other.transform.position = _teleportTarget.position;
		}
	}
}
