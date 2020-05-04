using System.Collections.Generic;
using Events;
using Other;
using UnityEngine;
using Utils.Event;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static Contexts Contexts => Instance._contexts;

        private GameEntity _player;

        public GameEntity Player
        {
            set
            {
                _player = value;
                _eventDispatcher.Send(GlobalEvent.PlayerSpawn, value);
            }
            get => _player;
        }

        private Contexts _contexts;
        private GameSystems _gameSystems;
        private FixedUpdateGameSystems _fixedUpdateGameSystems;

        private readonly EventDispatcher _eventDispatcher =
            new EventDispatcher();

        #region Static

        public static GameEntity GetEntity(int id)
        {
            return Contexts.game.GetEntityWithIdComp(id);
        }

        public static void AddHandler(short type, EventHandler handler)
        {
            Instance._eventDispatcher.AddHandler(type, handler);
        }

        public static void AddHandler<T>(short type, EventHandler<T> handler)
        {
            Instance._eventDispatcher.AddHandler(type, handler);
        }

        public static void RemoveHandler(short type, EventHandler handler)
        {
            Instance._eventDispatcher.RemoveHandler(type, handler);
        }

        public static void RemoveHandler<T>(short type, EventHandler<T> handler)
        {
            Instance._eventDispatcher.RemoveHandler(type, handler);
        }

        #endregion

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;

            _contexts = Contexts.sharedInstance;
            _contexts.SubscribeId();

            var physicsEntity = _contexts.physics.CreateEntity();
            physicsEntity.AddPhysicsComp(
                new List<CollisionInfo>());

            _gameSystems = new GameSystems(_contexts);
            _fixedUpdateGameSystems = new FixedUpdateGameSystems(_contexts);
        }

        private void Start()
        {
            _gameSystems.Initialize();
        }

        private void FixedUpdate()
        {
            _fixedUpdateGameSystems.Execute();
            _fixedUpdateGameSystems.Cleanup();
        }

        private void Update()
        {
            _gameSystems.Execute();
            _gameSystems.Cleanup();
        }

        private void OnDestroy()
        {
            _gameSystems.TearDown();
        }
    }
}