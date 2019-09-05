using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionAction
{
        Invalid = -1,
        PutInInventory = 0,
        Use = 1
}

public enum InteractionType
{
        Invalid = -1,
        Unique = 0,
        Accumulate = 1
}

public class InteractiveObj : MonoBehaviour
{
        #region PUBLIC VARS
        public Vector3 _rotAxis;
        public float _rotSpeed;
        public InteractionAction _interaction;
        public InteractionType _interactionType;
        #endregion
        #region PRIVATE VARS
        CustomGameObject _gameObjectInfo;
        ObjectInteraction _OnCloseEnough;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
                _gameObjectInfo = this.gameObject.GetComponent<CustomGameObject>();
                if (_gameObjectInfo)
                        _gameObjectInfo.validate();
        }

        // Update is called once per frame
        void Update()
        {

        }
}
