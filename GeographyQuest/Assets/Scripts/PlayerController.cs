using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
        #region PUBLIC VARS
        public Vector3 _moveDirection = Vector3.zero;
        public float _rotateSpeed;
        public float _moveSpeed;
        public float _speedSmoothing = 10.0f;
        #endregion

        // Update is called once per frame
        void Update()
        {
                UpdateMovement();
        }

        void UpdateMovement()
        {
                Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);

                cameraForward.y = 0f;
                cameraForward.Normalize();

                Vector3 cameraRight = new Vector3(cameraForward.z, 0f, -cameraForward.x);

                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");

                Vector3 targetDirection = horizontal * cameraRight + vertical * cameraForward;

                if (targetDirection != Vector3.zero)
                {
                        _moveDirection = Vector3.RotateTowards(_moveDirection, targetDirection,
                                _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
                        _moveDirection = _moveDirection.normalized;
                }

                // LERP
                float curSmooth = _speedSmoothing * Time.deltaTime;
                float targetSpeed = Mathf.Min(targetDirection.magnitude, 1f);
                _moveSpeed = Mathf.Lerp(_moveSpeed, targetSpeed, curSmooth);

                Vector3 displacement = _moveDirection * _moveSpeed * Time.deltaTime;

                this.GetComponent<CharacterController>().Move(displacement);
                transform.rotation = Quaternion.LookRotation(_moveDirection);
        }
}
