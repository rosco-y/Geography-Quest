using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

        #region PUBLIC VARS
        public GameObject _trackObj;
        public float _height;
        public float _desiredDistance;
        public float _heightDamp;
        public float _rotDamp;
        #endregion

        // Update is called once per frame
        void Update()
        {
                UpdateRotAndTrans();
        }

        void UpdateRotAndTrans()
        {
                  if (_trackObj)
                {
                        // target angle and height
                        float desiredRotationAngle = _trackObj.transform.eulerAngles.y;
                        float desiredHeight = _trackObj.transform.position.y + _height;

                        // actual angle and height.
                        float rotAngle = transform.eulerAngles.y;
                        float height = transform.position.y;

                        // LERP
                        rotAngle = Mathf.LerpAngle(rotAngle, desiredRotationAngle, _rotDamp);
                        height = Mathf.Lerp(height, desiredHeight, _heightDamp * Time.deltaTime);

                        // move camera
                        Quaternion curRotation = Quaternion.Euler(0f, rotAngle, 0f);
                        Vector3 pos = _trackObj.transform.position;
                        pos -= curRotation * Vector3.forward * _desiredDistance;
                        pos.y = height;
                        transform.position = pos;
                }
                else
                {
                        Debug.LogError("GameCamera:Error, _trackObj invalid.");
                }
        }
}
