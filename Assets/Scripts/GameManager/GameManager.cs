using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public partial class GameManager : Singleton<GameManager>
    {
        private const int INITIAL_EVENT_CAPACITY = 16;
        private const float BLOCKING_EVENT_HANDLING_COOLTIME = 1.0f;

        public GameManager.Properties properties { get; private set; }

        private bool _isGameStarted;

        private float _totalPlaytime;
        private float _elapsablePhasePlaytime;
        private int _killCount;
        private int _gold;

        private bool _blockingEventFlag;
        private float _blockingEventHandlingCooltimeLeft;
        private Queue<IEnumerator> _blockingEventHandlers;

        private PhaseRuntimeState _phaseExecutionResult;

        private PhaseRuntime _phaseRuntimeTree;

        private Camera _mainCamera;
        private TestPlayer _testPlayer;

        private Transform _runtimeContainer;

        public void Subscribe(TestPlayer testPlayer)
        {
            _testPlayer = testPlayer;
        }

        private void TraceCamera()
        {
            if (_testPlayer == null)
                return;

            float limit = 2.0f;
            float traceSpeed = 1.5f;

            Vector2 source = _mainCamera.transform.position;
            Vector2 destination = _testPlayer.transform.position;

            Vector2 next = Vector2.Lerp(source, destination, traceSpeed * Time.deltaTime);

            if (Vector2.Distance(next, destination) > limit)
                next = next.normalized * limit;

            Vector3 camPosition = new Vector3(next.x, next.y, _mainCamera.transform.position.z);
            _mainCamera.transform.position = camPosition;
        }

        private void LateUpdate()
        {
            TraceCamera();
        }

        private void Awake()
        {
            _blockingEventHandlers = new Queue<IEnumerator>(INITIAL_EVENT_CAPACITY);
            properties = new GameManager.Properties(this);
            _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            _runtimeContainer = new GameObject("@Runtime Container").transform;

            DontDestroyOnLoad(_runtimeContainer);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (_isGameStarted)
            {
                HandleEvents();
                UpdatePhaseRuntimeTree();
            }
        }

        public void StartGame()
        {
            if (_isGameStarted)
                return;

            _isGameStarted = true;

            StartPhaseRuntimeTree("Phases/Test/New Stage");
            CreatePlayer("TestPlayer");
            _mainCamera.transform.position = 10.0f * Vector3.back;
        }

        private void CreatePlayer(string resourcePath)
        {
            GameObject resource = Resources.Load(resourcePath) as GameObject;
            GameObject instance = GameObject.Instantiate(resource);
            instance.name = "Player";
            instance.transform.parent = _runtimeContainer.transform;
            instance.transform.position = Vector3.zero;
        }

        public void EndGame()
        {
            _isGameStarted = false;

            Transform[] children = _runtimeContainer.GetComponentsInChildren<Transform>();

            for (int i = 0; i < children.Length; ++i)
            {
                Destroy(children[i].gameObject);
            }

            UIManager.Instance.GameResultCanvas.Show();
        }

        public void PublishEvent(IEnumerator eventHandler)
        {
            _blockingEventHandlers.Enqueue(eventHandler);
        }

        private void HandleEvents()
        {
            if (_blockingEventFlag)
                return;
            else if (_blockingEventHandlingCooltimeLeft > 0.0f)
                _blockingEventHandlingCooltimeLeft -= Time.deltaTime;
            else if (_blockingEventHandlers.Count > 0)
                StartCoroutine(HandleBlockingEvent(_blockingEventHandlers.Dequeue()));
        }

        private IEnumerator HandleBlockingEvent(IEnumerator eventHandler)
        {
            _phaseRuntimeTree.Pause();
            _blockingEventFlag = true;
            yield return StartCoroutine(eventHandler);
            _blockingEventHandlingCooltimeLeft = BLOCKING_EVENT_HANDLING_COOLTIME;
            _blockingEventFlag = false;
            _phaseRuntimeTree.Resume();
        }

        private void StartPhaseRuntimeTree(string phaseSoResourcePath)
        {
            if (_phaseRuntimeTree != null)
                return;

            PhaseSO phaseSO = Resources.Load(phaseSoResourcePath) as PhaseSO;

            _phaseRuntimeTree = PhaseRuntimeFactory.CreateRuntime(phaseSO, properties);
            _phaseRuntimeTree.Start();
        }

        private void UpdatePhaseRuntimeTree()
        {
            if (_phaseRuntimeTree == null)
                return;

            _phaseRuntimeTree.Update();

            PhaseRuntimeState phaseRuntimeState = _phaseRuntimeTree.CheckPhaseRuntimeState();

            switch (phaseRuntimeState)
            {
                case PhaseRuntimeState.Continue:
                    // NOTE: This case has intentionally no operation.
                    break;

                case PhaseRuntimeState.Pass:
                    _phaseRuntimeTree.End();
                    _phaseExecutionResult = phaseRuntimeState;

                    if (_phaseRuntimeTree.TrySearchNextRuntime())
                        _phaseRuntimeTree.Start();
                    else
                    {
                        _phaseRuntimeTree = null;
                        EndGame();
                    }
                    break;

                case PhaseRuntimeState.Fail:
                    _phaseRuntimeTree.End();
                    _phaseRuntimeTree = null;
                    _phaseExecutionResult = phaseRuntimeState;
                    EndGame();
                    break;

                default:
                    Debug.Assert(false, "Unknown case handling occured.");
                    break;
            }
        }
    }
}