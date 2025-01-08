using System;
using System.Collections;
using UnityEngine;

namespace Unchord
{
    public partial class GameManager
    {
        public class Properties
        {
            #region Read Only Properties
            public float TotalPlaytime => _gm._totalPlaytime;
            public PhaseRuntimeState PhaseExecutionResult => _gm._phaseExecutionResult;
            public bool BlockingEventFlag => _gm._blockingEventFlag;
            public Camera MainCamera => _gm._mainCamera;
            #endregion

            #region Write Only Properties
            #endregion

            #region Read and Write Rroperties
            public float ElapsablePhasePlaytime { get => _gm._elapsablePhasePlaytime; set => _gm._elapsablePhasePlaytime = value; }
            #endregion

            private GameManager _gm;

            public Properties(GameManager gm)
            {
                _gm = gm;
            }

            public void PublishEvent(IEnumerator eventHandler)
            {
                _gm.PublishEvent(eventHandler);
            }
        }
    }
}