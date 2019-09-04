using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
        public GameObject _trackObj;
        public float _height;
        public float _desiredDistance;
        public float _heightDamp;
        public float _rotDamp;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
                UpdateRotAndTrans();
        }

        void UpdateRotAndTrans()
        {
                // check to see that _trackObj is set in inspector.
                if (_trackObj)
                {
                        /**************************************************************
                         * store the desired angle and height in local variables, so
                         * we can lerp towards them.
                         **************************************************************/
                        float desiredRotationAngle = _trackObj.transform.eulerAngles.y;
                        float desiredHeight = _trackObj.transform.position.y + _height;

                        /****************************************************************
                         * also store current roation and height for lerping.
                         ****************************************************************/
                        float rotAngle = transform.eulerAngles.y;
                        float height = transform.position.y;

                        /****************************************************************
                         * Now we are ready to lerp towards our desired height and 
                         * rotation.
                         ****************************************************************/
                        rotAngle = Mathf.LerpAngle(rotAngle, desiredRotationAngle, _rotDamp);
                        height = Mathf.Lerp(height, desiredHeight, _heightDamp * Time.deltaTime);

                        Quaternion currentRotation = Quaternion.Euler(0f, rotAngle, 0f);
                        Vector3 pos = _trackObj.transform.position;
                        pos -= currentRotation * Vector3.forward * _desiredDistance;
                        pos.y = height;
                        transform.position = pos;

                        transform.LookAt(_trackObj.transform.position);
                }
                else
                {
                        Debug.Log("GameCamera: Error, invalid _trackObj");
                }
        }
}
