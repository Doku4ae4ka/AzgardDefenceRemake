// using System;
// using Sirenix.OdinInspector;
// using Source.Scripts.ECS.Components;
// using Source.Scripts.SignalSystem;
// using Source.SignalSystem;
// using UnityEditor.Rendering.LookDev;
// using UnityEngine;
//
namespace Source.Scripts.Signalizators
{
//     [Serializable]
//     public class CameraController : SignalizeListener<
//         OnCameraChangedStateSignal, 
//         OnGameplayChangedStateSignal>
//     {
//         [SerializeField] private float lerpFactor = 3f;
//         [SerializeField] private float flyOffsetMultiply = 6f;
//         [SerializeField] private bool returnToTargetOnStop;
//         [SerializeField, ReadOnly] private CameraState state = CameraState.Fly;
//         [SerializeField, ReadOnly] private Transform followTarget;
//         [SerializeField, ReadOnly] private bool hasTarget;
//         [SerializeField, ReadOnly] private Camera camera;
//         [SerializeField, HideInInspector] private float _lerpFactorMoving;
//         [SerializeField, HideInInspector] private float _lerpFactorStop;
//         private CameraMovable _cameraMovable;
//         public const float LerpMultiply = 0.01f;
//
//         public override void Initialize()
//         {
//             camera = Camera.main;
//             _lerpFactorMoving = lerpFactor * LerpMultiply;
//             var stopMultiply = 0.3f;
//             _lerpFactorStop = lerpFactor * LerpMultiply * stopMultiply;
//             _cameraMovable = data.Hero.PlayerMovable;
//         }
//
//         protected override void OnSignal(OnPlayerSpawnSignal data)
//         {
//             if (!data.Hero.IsOwner) return;
//             _cameraMovable = data.Hero.PlayerMovable;
//             followTarget = data.Hero.transform;
//             hasTarget = true;
//             FollowUpdate();
//         }
//
//         protected override void OnSignal(OnPlayerDespawnSignal data)
//         {
//             if (!data.Hero.IsOwner) return;
//             
//             TryStopFollow();
//         }
//
//         private void TryStopFollow()
//         {
//             followTarget = null;
//             hasTarget = false;
//         }
//
//         protected override void OnSignal(OnCameraChangedStateSignal data)
//         {
//             state = data.State;
//         }
//
//         protected override void OnSignal(OnGameplayChangedStateSignal data)
//         {
//             if(data.Result != OnGameplayChangedStateSignal.ResultType.Pause) TryStopFollow();
//         }
//
//         public override void Update()
//         {
//             if (!hasTarget) return;
//             if (state == CameraState.Follow) FollowUpdate();
//         }
//         
//         public override void FixedUpdate()
//         {
//             if (!hasTarget) return;
//             if (state == CameraState.Fly) FlyUpdate();
//         }
//
//         private void FollowUpdate()
//         {
//             var targetPosition = followTarget.position;
//             targetPosition.z = camera.transform.position.z;
//             camera.transform.position = targetPosition;
//         }
//         
//         private void FlyUpdate()
//         { 
//             var targetPosition = followTarget.position;
//             var lerpFactorResult = _lerpFactorStop;
//             if (_cameraMovable.IsMoving)
//             {
//                 targetPosition.x += _cameraMovable.InputAxis.x * flyOffsetMultiply;
//                 targetPosition.y += _cameraMovable.InputAxis.y * flyOffsetMultiply;
//                 lerpFactorResult = _lerpFactorMoving;
//             }
//             else if (!returnToTargetOnStop) return;
//             
//             targetPosition.z = camera.transform.position.z;
//             camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, lerpFactorResult);
//         }
//         
//     }
//     
     public enum CameraState
     {
         Stay, 
         Follow, 
         Fly
     }
}