// using System;
// using Sirenix.OdinInspector;
// using Source.Scripts.ECS.Components;
// using Source.Scripts.Managers.ProjectSettings;
// using Source.Scripts.SignalSystem;
// using Source.SignalSystem;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.InputSystem;
//
// namespace Source.Scripts.Signalizators
// {
//     [Serializable]
//     public class Inputer : SignalizeListener<OnGameplayChangedStateSignal>
//     {
//         [SerializeField, ReadOnly] private CameraMovable cameraMovable;
//         [SerializeField] private bool isGamepadActivated;
//         
//         private Input _input;
//         private int _tryShootSum = 0;
//
//         private Camera _mainCamera;
//         private Vector2 _aimDirection = Vector2.zero;
//         private float _shootingMagnitude;
//         private bool _isGameStop = false;
//         private float _playerStopDealy = 0.09f;
//         private float _playerStopTimer;
//
//         private void UpdateCurrentControlScheme(InputAction.CallbackContext context)
//         {
//             isGamepadActivated = context.control.device == Gamepad.current;
//         }
//         
//         public override void Initialize()
//         {
//             _input = new();
//         }
//
//         public void ActivateInput()
//         {
//             if (_input.IsActivated) return;
//             _input.Activate();
//             _input.Character.Look.performed += OnAim;
//             _input.Character.CameraMove.performed += OnInputMove;
//             _input.Character.Interact.performed += OnInvokeShoot;
//             _input.Character.Cancel.performed += OnTryInteract;
//         }
//
//         public void DeactivateInput()
//         {
//             _input.Character.Look.performed += OnAim;
//             _input.Character.CameraMove.performed += OnInputMove;
//             _input.Character.Interact.performed += OnInvokeShoot;
//             _input.Character.Cancel.performed += OnTryInteract;
//             _input.Deactivate();
//         }
//
//         public void OnInvokeShoot(InputAction.CallbackContext context)
//         {
//             if (_isGameStop) return;
//             UpdateCurrentControlScheme(context);
//             if (context.control.IsPressed())
//             {
//                 _tryShootSum++;
//             }
//         }
//
//         public void OnInputMove(InputAction.CallbackContext context)
//         {
//             if (_isGameStop) return;
//             _playerStopTimer = 0;
//             UpdateCurrentControlScheme(context);
//             cameraMovable.SetInputAxis(context.ReadValue<Vector2>());
//             cameraMovable.Move();
//         }
//
//         public void OnAim(InputAction.CallbackContext context)
//         {
//             if (_isGameStop) return;
//             UpdateCurrentControlScheme(context);
//             
//             if (isGamepadActivated)
//             {
//                 if (context.ReadValue<Vector2>().magnitude > 0f)
//                 {
//                     _shootingMagnitude = context.ReadValue<Vector2>().magnitude;
//                     _aimDirection = context.ReadValue<Vector2>().normalized;
//                 }
//                 else
//                 {
//                     _shootingMagnitude = 0.001f; // сбрасывается чтобы небыло с авто стрельбы при отпускании джойстика
//                 }
//             }
//             else
//             {
//                 Vector2 mousePositionScreen = _mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
//                 _aimDirection = (mousePositionScreen - (Vector2)playerMovable.transform.position).normalized;
//                 if (_aimDirection.x > 0)
//                 {
//                     
//                 }
//             }
//             
//             //weaponHandler.RefreshWeaponDirection(_direction, _shootingMagnitude);
//         }
//
//         public void OnTryInteract(InputAction.CallbackContext context)
//         {
//             UpdateCurrentControlScheme(context);
//             
//             Signal.RegistryRaise(new CommandInteractablePressedBroadcast());
//             InstanceFinder.ClientManager.Broadcast(new CommandInteractablePressedBroadcast
//                 { Hero = _hero });
//         }
//
//
//         public void OnTryUlt(InputAction.CallbackContext context)
//         {
//             UpdateCurrentControlScheme(context);
//             
//             InstanceFinder.ClientManager.Broadcast(new OnTryUltBroadcast
//                 { OriginCharacterEntity = _hero.CharacterEntity, Direction = _aimDirection });
//         }
//
//         public override void FixedUpdate()
//         {
//             if (!_input.IsActivated) return;
//
//             _playerStopTimer += Time.fixedDeltaTime;
//             if (_playerStopTimer > _playerStopDealy && playerMovable.IsMoving) playerMovable.Stop();
//             if (_input.Character.Move.IsPressed()) _playerStopTimer = 0;
//             playerAnimationController.Parameters.IsLookingRight = _aimDirection.x > 0;
//             if (isGamepadActivated || BuildSetup.isMobile)
//             {
//                 if (_shootingMagnitude > NoneShootLimitMagnitude)
//                 {
//                     _tryShootSum = 0;
//                    // weaponHandler.Shoot();
//                 }
//             }
//             else if (!IsPointerOverUIElement() && _tryShootSum > 0)
//             {
//                 _tryShootSum = 0;
//                // weaponHandler.Shoot();
//             }
//         }
//
//         private bool IsPointerOverUIElement()
//         {
//             if (EventSystem.current == null) return false;
//
//             // Проверяем, есть ли указатель мыши над элементом UI
//             return EventSystem.current.IsPointerOverGameObject();
//         }
//
//         // protected override void OnSignal(OnPlayerSpawnSignal data)
//         // {
//         //     if (!data.Hero.IsOwner) return;
//         //
//         //     _mainCamera = Camera.main;
//         //     _hero = data.Hero;
//         //     playerMovable = data.Hero.PlayerMovable;
//         //     playerAnimationController = data.Hero.GetComponentInChildren<AnimationController>();
//         //
//         //     ActivateInput();
//         // }
//
//         // protected override void OnSignal(OnPlayerDeadSignal data)
//         // {
//         //     if (!data.Hero.IsOwner) return;
//         //     playerMovable.Stop();
//         //     DeactivateInput();
//         // }
//
//         // protected override void OnSignal(OnPlayerStateChangedSignal data)
//         // {
//         //     if (data.Player != _hero) return;
//         //     
//         //     if (data.CurrentState != Constants.StateMachine.Name.Input)
//         //     {
//         //         DeactivateInput();
//         //         playerMovable.SetInputAxis(Vector2.zero);
//         //     }
//         //     else ActivateInput();
//         // }
//
//         protected override void OnSignal(OnGameplayChangedStateSignal data)
//         {
//             _isGameStop = true;
//             DeactivateInput();
//         }
//
//         // protected override void OnSignal(OnPlayerDespawnSignal data)
//         // {
//         //     if (!data.Hero.IsOwner) return;
//         //     DeactivateInput();
//         // }
//
//         public class Input : Controls
//         {
//             public bool IsActivated { get; private set; }
//
//             public void Activate()
//             {
//                 Enable();
//                 IsActivated = true;
//             }
//
//             public void Deactivate()
//             {
//                 Disable();
//                 IsActivated = false;
//             }
//         }
//     }
// }