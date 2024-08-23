// using Exerussus._1EasyEcs.Scripts.Core;
// using Sirenix.OdinInspector;
// using Source.Scripts.ECS.Core;
// using UnityEngine;
//
// namespace Source.Scripts.ECS.Components
// {
//     [RequireComponent(typeof(Rigidbody2D))]
//     [AddComponentMenu("ECS/Components/CameraMovable")]
//     public class CameraMovable : EcsComponent
//     {
//         [SerializeField, HideInInspector] private Rigidbody2D _rigidbody;
//         [SerializeField] private float defaultSpeed = 5;
//         [SerializeField, ReadOnly] private float currentSpeed = 5;
//         
//         public bool IsStopped { get; private set; }
//         public Vector2 InputAxis { get; private set; }
//         public bool IsInitialized { get; private set; }
//         public bool IsMoving { get; private set; }
//
//         public float CurrentSpeed => currentSpeed;
//
//         public float DefaultSpeed => defaultSpeed;
//
//         [SerializeField] private bool _isSliding = false;
//
//         public bool IsSliding
//         {
//             get => _isSliding;
//             set => _isSliding = value;
//         }
//
//         [SerializeField] private float slideAcceleration = 10;
//         [SerializeField] private float slideFriction = 0.95f;
//
//         public Rigidbody2D Rigidbody => _rigidbody;
//         
//         public void SetMoveSpeed(float value)
//         {
//             currentSpeed = value;
//         }
//         
//         private void SlidingMove()
//         {
//             _rigidbody.velocity = slideFriction * (_rigidbody.velocity + slideAcceleration * Time.fixedDeltaTime * InputAxis);
//         }
//         
//         public void SetInputAxis(Vector2 inputAxis)
//         {
//             InputAxis = inputAxis;
//         }
//
//         public void Move()
//         {
//             if (_isSliding)
//             {
//                 SlidingMove();
//                 IsMoving = false;
//                 return;
//             }
//
//             Vector2 resultAxis;
//             if (InputAxis.magnitude <= 1) resultAxis = InputAxis;
//             else resultAxis = InputAxis.normalized;
//             _rigidbody.velocity = resultAxis * currentSpeed;
//             IsMoving = _rigidbody.velocity.x != 0 || _rigidbody.velocity.y != 0;
//         }
//
//         public void Stop()
//         {
//             if (_isSliding) return;
//             IsMoving = false;
//             _rigidbody.velocity = Vector2.zero;
//         }
//         
//         protected override void OnValidate()
//         {
//             base.OnValidate();
//             IsInitialized = true;
//             if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
//             currentSpeed = defaultSpeed;
//         }
//
//         public override void Initialize(int entity, Componenter componenter)
//         {
//             ref var data = ref componenter.Add<CameraMovableData>(entity);
//             data.Value = this;
//         }
//
//         public void StopSliding()
//         {
//             _isSliding = false;
//         }
//         
//         public void Push(Vector2 addedVelocity)
//         {
//             _isSliding = true;
//             _rigidbody.velocity += addedVelocity;
//         }
//     }
//     
//     public struct CameraMovableData
//     {
//         public CameraMovable Value;
//     }
// }