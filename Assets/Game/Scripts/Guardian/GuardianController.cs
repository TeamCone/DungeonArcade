using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class GuardianController: MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        [SerializeField] private Transform _guardianPathHolder;
        [SerializeField] private float _speed;
        private List<Transform> _guardianPaths = new List<Transform>();
        
        

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();

            foreach (Transform child in _guardianPathHolder)
            {
                _guardianPaths.Add(child);
            }
        }

        private void Start()
        {
            StartMovement();
        }

        private void StartMovement()
        {
            TweenFacade.LocalMove(_transform, GetRandomPath(),_speed,StartMovement,true);
        }

        private Vector3 GetRandomPath()
        {
            var randomIndex = UnityEngine.Random.Range(0, _guardianPaths.Count-1);
            return _guardianPaths[randomIndex].position;
        }

 


    }
}