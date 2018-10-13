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

	public async void Throw()
	{
		_transform.parent = null;
		CreateRigidBody2D();
		_throwItem.Throw();
		SetState(EnumItemState.MOVING);
		TweenFacade.ThrowItem(_spriteRenderer, ThrowTime);
		await SetBackToIdle();
		
	}
	
	//set item back to idle after n seconds
	private IEnumerator SetBackToIdle()
	{
		yield return  new WaitForSeconds(ThrowTime);
		SetState(EnumItemState.IDLE);
	}
	
	
}
