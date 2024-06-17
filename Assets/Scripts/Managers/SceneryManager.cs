using DataSources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenery
{
    public class SceneryManager : MonoBehaviour
    {
        [SerializeField] private DataSource<SceneryManager> _sceneryManagerSource;

        private void OnEnable()
        {
            if (_sceneryManagerSource != null)
                _sceneryManagerSource.Reference = this;
        }

        private void OnDisable()
        {
            if (_sceneryManagerSource != null && _sceneryManagerSource == this)
                _sceneryManagerSource.Reference = null;
        }
    }
}