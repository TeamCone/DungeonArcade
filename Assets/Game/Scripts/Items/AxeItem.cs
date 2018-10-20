using Game.Player;
using UnityEngine;

public class AxeItem : MonoBehaviour, IThrowItem
{
	private Vector3 _velocty;
	private float _throwSpeed;
	public void Throw(bool isFacingRight)
	{
		if (isFacingRight == false)
		{
			_throwSpeed = -60;
		}
		else
		{
			_throwSpeed = 60;
		}
		var itemRigidbody = GetComponent<Rigidbody2D>();
		_velocty = new Vector3(_throwSpeed,0,0);
		itemRigidbody.AddForce (_velocty, ForceMode2D.Impulse);
	}


	public string Name()
	{
		return "AxeItem";
	}
}
