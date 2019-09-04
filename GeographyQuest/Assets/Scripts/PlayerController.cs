using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
        #region INSPECTOR-SET VARIABLES:
        // moveDirection
        public Vector3 _moveDirection = Vector3.zero;
        // move and Rotate speed
        public float _rotateSpeed;
        public float _moveSpeed = 0f;
        public float _speedSmoothing = 10f;
        #endregion

        // Update is called once per frame
        void Update()
        {
                updateMovement();
        }

        void updateMovement()
        {
                /*********************************************************
                 * store horizontal projection of the forward and right 
                 * vectors of the current camera
                 ********************************************************/
                Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);
                /*********************************************************
                 * 
                 *********************************************************/

        }
}
