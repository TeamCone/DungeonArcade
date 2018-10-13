using System.Collections;
using Game.Player;
using UnityEngine;

public class ItemController : MonoBehaviour, IItem
{
	private IThrowItem _throwItem;
	private EnumItemState _enumItemState;
	private EnumPlayer _origin;
	private Rigidbody2D _rigidbody2D;
	private SpriteRenderer _spriteRenderer;
	private Transform _transform;
	private const float ThrowTime = 2f;
	
	private void Awake()
	{
		_transform = GetComponent<Transform>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_throwItem = GetComponent<IThrowItem>();
		_enumItemState = EnumItemState.IDLE;	
	}

	private void CreateRigidBody2D()
	{
		_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
		_rigidbody2D.mass = 5;
		_rigidbody2D.gravityScale = 5;
		_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		_rigidbody2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
		_rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	public bool IsThrowable()
	{
		return _throwItem.Name() != "TreasureItem";
	}

	public EnumPlayer GetOrigin()
	{
		return _origin;
	}

	public void SetOrigin(EnumPlayer player, Transform itemHolder)
	{
		_origin = player;
		_transform.position = itemHolder.position;
		Destroy(_rigidbody2D);
		_transform.parent = itemHolder;
	}

	public EnumItemState GetState()
	{
		return _enumItemState;
	}

	public void SetState(EnumItemState state)
	{
		_enumItemState = state;
	}

	public void RemoveItem()
	{
		_transform.parent = null;
		CreateRigidBody2D();

		if (IsThrowable() == false)
		{
			Debug.Log("ITEM IS TREASURE");
			SetState(EnumItemState.IDLE);
		}
	}

	public async void Throw(bool isFacingRight)
	{
		RemoveItem();
		_throwItem.Throw(isFacingRight);
		SetState(EnumItemState.MOVING);
		TweenFacade.ThrowItemEffect(_spriteRenderer, ThrowTime);
		await SetBackToIdle();
		
	}
	
	//set item back to idle after n seconds
	private IEnumerator SetBackToIdle()
	{
		yield return  new WaitForSeconds(ThrowTime);
		SetState(EnumItemState.IDLE);
	}
	
	
}
