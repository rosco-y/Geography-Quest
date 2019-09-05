using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CustomObjectType
{
        Invalid = -1,
        Unique = 0,
        Coin = 1,
        Ruby = 2,
        Emerald = 3,
        Diamond = 4
}
public class CustomGameObject : MonoBehaviour
{
        #region PUBLIC VARS
        public CustomObjectType _objectType;
        public string _displayName;
        #endregion
        void Start()
        {

        }

        public void validate()
        {
                if (_displayName == "")
                        _displayName = "unamed object";
        }

}
