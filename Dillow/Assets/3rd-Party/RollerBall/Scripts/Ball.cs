using System;
using UnityEngine;

//namespace UnityStandardAssets.Vehicles.Ball
//{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
        [SerializeField] private float m_JumpPower = 5;
        [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        private float drag;

		[SerializeField] private float fallMultiplier = 2.5f;
		[SerializeField] private float lowJumpMultiplier = 2f;
		[SerializeField] private float skidMultiplier = 4f;
		[SerializeField] private float waterPowerMultiplier = 0.5f;
	    [SerializeField] private bool enableBuyoancy = false;

		private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;

        private Vector3 jumpVector;

		private int gravityDisabledCount;
		private bool inWater;
		private bool inWaterLF;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocity;
            Physics.gravity = -Vector3.up * 20f;
            jumpVector = Vector3.up;
			drag = m_Rigidbody.drag;
			gravityDisabledCount = 0;
			inWater = false;
			inWaterLF = inWater;
        }

		public void FixedUpdate () {
		if (!enableBuyoancy) return;
			float amountAboveSeaLevel = m_Rigidbody.position.y - 6.25f;
			if (amountAboveSeaLevel < 0f) { //Underwater
				inWater = true;
				if (inWater && !inWaterLF) {
					m_Rigidbody.drag = drag * 20f;
					DisableGravity();
				}
				m_Rigidbody.AddForce(Vector3.up * Physics.gravity.y * (amountAboveSeaLevel));
			} else {
				inWater = false;
				if (!inWater && inWaterLF) {
					m_Rigidbody.drag = drag;
					EnableGravity();
				}
			}
			inWaterLF = inWater;
		}
		
		public void EnableGravity () {
			print("Attempting to enable gravity...");    
		
		    if (gravityDisabledCount > 0) {
				print("Enabling gravity...");
				m_Rigidbody.useGravity = true;
				gravityDisabledCount--;
			} else {
				print("Failed. Gravity was already enabled...");
			}	
	}

		public void DisableGravity () {
			print("Attempting to disable gravity...");    
			if (gravityDisabledCount == 0) {
				print("Disabling gravity...");
				m_Rigidbody.useGravity = false;
			} else {
			print("Failed. Gravit was already disabled...");
			}
			gravityDisabledCount++;
		}

		public void Move(Vector3 moveDirection)
        {
			//print("MoveDirection: " + moveDirection);
			// If using torque to rotate the ball...

			if (inWater) {
				m_Rigidbody.AddForce(moveDirection * m_MovePower * waterPowerMultiplier);

				if (m_UseTorque) {
					m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
				}
			} else {
				if (m_UseTorque) {
					// ... add torque around the axis defined by the move direction.
					m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
				} else {
					// Otherwise add force in the move direction.
					m_Rigidbody.AddForce(moveDirection * m_MovePower);
				}
			}

            //Calculate jump direction
            if (m_Rigidbody.velocity.magnitude > 10f) {
				if (Vector3.Cross(m_Rigidbody.angularVelocity, m_Rigidbody.velocity).y > 0f) {
					//adjustedJumpHeight = m_JumpHeight * 5f;
					jumpVector = (-m_Rigidbody.velocity.normalized + Vector3.up * 2f).normalized;
					//jumpVector = (-new Vector3(m_Rigidbody.velocity.x, 0f, m_Rigidbody.velocity.z) + Vector3.up * 2f).normalized;
				} else {
					jumpVector = Vector3.up;
				}
            } else {
                //adjustedJumpHeight = m_JumpHeight;
                jumpVector = Vector3.up;
            }
		}

        public void Jump() {
            // If on the ground and jump is pressed...
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, k_GroundRayLength))
            {
                    m_Rigidbody.AddForce(jumpVector * m_JumpPower, ForceMode.Impulse);
            }

            if (m_Rigidbody.velocity.y < 0f)
            {
                m_Rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
            }
            if (m_Rigidbody.velocity.y > 0f)
            {
                m_Rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
            }
        }

        //public void OnDrawGizmos () {
        //    Gizmos.color = Color.blue;
        //    //Gizmos.DrawRay(transform.position, transform.position + jumpVector * m_JumpPower);
        //    //Vector3 cross = Vector3.Cross(m_Rigidbody.angularVelocity, m_Rigidbody.velocity);
        //    //Gizmos.DrawRay(transform.position, jumpVector * adjustedJumpHeight);
        //}
    }
//}
