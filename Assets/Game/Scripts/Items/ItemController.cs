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
	private BoxCollider2D _boxCollider2D;
	
	private void Awake()
	{
		_transform = GetComponent<Transform>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_throwItem = GetComponent<IThrowItem>();
		_boxCollider2D = GetComponent<BoxCollider2D>();
		_enumItemState = EnumItemState.IDLE;	
	}

	private void CreateRigidBody2D()
	{
		_boxCollider2D.enabled = true;
		_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
		_rigidbody2D.mass = 5;
		_rigidbody2D.gravityScale = 5;
		_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		_rigidbody2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
		_rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
		
	}

	//temporary fix
	void Update()
	{
		if (_transform.parent != null)
		{
			SetState(EnumItemState.PICKED);
		}
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
		_boxCollider2D.enabled = false;
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

	public void Throw(bool isFacingRight)
	{
		
		RemoveItem();
		_throwItem.Throw(isFacingRight);
		SetState(EnumItemState.MOVING);
		TweenFacade.ThrowItemEffect(_spriteRenderer);
		
	}

	
	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			ItemToIdle();
		}
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			ItemToIdle();
		}
	}

	private void ItemToIdle()
	{
		TweenFacade.StopThrowItemEffect(_spriteRenderer);
		_rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
		SetState(EnumItemState.IDLE);
	}
	
	
}
