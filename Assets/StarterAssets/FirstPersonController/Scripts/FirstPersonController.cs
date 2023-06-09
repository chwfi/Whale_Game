﻿using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using DG.Tweening;

#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		public AudioClip LandingAudioClip;
		public AudioClip[] FootstepAudioClips;
		[Range(0, 1)] public float FootstepAudioVolume = 0.5f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;
	
#if ENABLE_INPUT_SYSTEM
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		private bool _hasAnimator;
		public bool CanRotateCam = true;

		private int _animIDSpeed;
		private int _animIDJump;
		private int _animIDFreeFall;
		private int _animIDSwim;
		private int _animIDMotionSpeed;
		private int _animIDPickup;
		private int _animIDHold;
		private int _animIDSwing;

		[SerializeField] private Transform _whalePos;
		[SerializeField] private Transform _homePos;

		private float _clampedDistance;
		[SerializeField] private float _maxDis = 10;

		private float _animationBlend;
		private Animator _animator;
		private PlayerStatManager _playerManager;
		private Glider _glider;

		public GameObject Arm;


		public bool CanMove = true;

		private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_animator = GetComponentInChildren<Animator>();
			_playerManager = GetComponent<PlayerStatManager>();
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
			_playerInput = GetComponent<PlayerInput>();
			_glider = GetComponentInChildren<Glider>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;

			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDSwim = Animator.StringToHash("Swim");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
			_animIDPickup = Animator.StringToHash("Pickup");
			_animIDHold = Animator.StringToHash("Hold");
			_animIDSwing = Animator.StringToHash("Hammer");
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			if (CanMove)
				Move();
			UpDownInZeroGravity();
			SetParent();
			ReturnToHome();

            if (isZeroGravity)
                _animator.SetBool(_animIDSwim, true);
            else
                _animator.SetBool(_animIDSwim, false);
        }

		private void LateUpdate()
		{
			if (CanRotateCam)
				CameraRotation();
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}

			if (_speed >= 2 && _speed < 4)
            {
				_animator.SetFloat(_animIDSpeed, 2);
            }
			else if (_speed < 2)
            {
				_animator.SetFloat(_animIDSpeed, 0);
            }
			else if (_speed >= 4)
            {
				_animator.SetFloat(_animIDSpeed, 4);
            }

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		public void PickupAnimation(bool value)
        {
			_animator.SetBool(_animIDPickup, value);
		}

		public void DisableGlider()
        {
			_glider.Disable();
		}

		public void ReturnToHome()
		{
			float dis = Vector3.Distance(_whalePos.position, transform.position);

			if (Input.GetKeyDown(KeyCode.G) && isZeroGravity)
			{
				enableGlider = false;
				transform.DOMove(_homePos.position, dis / 20);
				//Gravity = -15;
				if (InventoryManager.Instance.GliderCount >= 1)
					SoundManager.Instance.EngineOff();
				_glider.Disable();

				if (Grounded) _glider.Disable();
			}
		}

		public float timer;
		public bool isZeroGravity = false;
		bool enableGlider = false;

		public float zeroGravity = -0.01f;
		[SerializeField] private float maxTime = 3f;

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				UIManager.Instance.OffSpaceInfo();
				Arm.SetActive(true);
				enableGlider = true;
				_playerManager.grounded = true;
				Gravity = -12f;
				timer = 0f;
				isZeroGravity = false;
			}
			else if (isZeroGravity)
			{
				UIManager.Instance.InSpaceInfo();
				_playerManager.grounded = false;
				if (enableGlider)
					Gravity = zeroGravity;
				if (InventoryManager.Instance.GliderCount >= 1 && enableGlider)
                {
					SoundManager.Instance.EngineOnAndRun();
					_glider.Enable();
					Arm.SetActive(false);
                }
			}

			if (!Grounded)
			{
				if (!isZeroGravity)
					timer += Time.unscaledDeltaTime;
				if (timer >= maxTime)
				{
					isZeroGravity = true;
					_verticalVelocity = Mathf.Sqrt(JumpHeight * 0f * Gravity);
					timer = 0f;
				}
			}

			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// update animator if using character
				if (_hasAnimator)
				{
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

					// update animator if using character
					if (_hasAnimator)
					{
                        _animator.SetBool(_animIDJump, true);
                    }
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimator)
					{
                        _animator.SetBool(_animIDFreeFall, true);
                    }
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		public void SetParent()
		{
			if (Grounded)
				transform.SetParent(_whalePos);
			else if (isZeroGravity)
				transform.SetParent(null);
		}

		private void UpDownInZeroGravity()
		{
			float _upDownSpeed = 1.4f;
			Vector3 upDir = Vector3.up * _upDownSpeed;
			Vector3 downDir = Vector3.down * _upDownSpeed;

			if (isZeroGravity && Input.GetKey(KeyCode.Space))
			{
				_verticalVelocity = upDir.sqrMagnitude;
                _animator.SetFloat(_animIDSpeed, 2);
            }
			else if (isZeroGravity && Input.GetKeyUp(KeyCode.Space))
			{
				_verticalVelocity = Vector3.zero.sqrMagnitude;
                _animator.SetFloat(_animIDSpeed, 0);
            }

			if (isZeroGravity && Input.GetKey(KeyCode.LeftControl))
			{
				_verticalVelocity = -downDir.sqrMagnitude;
                _animator.SetFloat(_animIDSpeed, 2);
            }
			else if (isZeroGravity && Input.GetKeyUp(KeyCode.LeftControl))
			{
				_verticalVelocity = Vector3.zero.sqrMagnitude;
                _animator.SetFloat(_animIDSpeed, 0);
            }
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnFootstep(AnimationEvent animationEvent)
		{
			if (animationEvent.animatorClipInfo.weight > 0.5f)
			{
				if (FootstepAudioClips.Length > 0)
				{
					var index = Random.Range(0, FootstepAudioClips.Length);
					AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
				}
			}
		}

		private void OnLand(AnimationEvent animationEvent)
		{
			if (animationEvent.animatorClipInfo.weight > 0.5f)
			{
				AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
			}
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}