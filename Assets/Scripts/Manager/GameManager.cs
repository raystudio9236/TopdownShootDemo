using System.Collections.Generic;
using Entitas;
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

        private readonly Dictionary<ActorTag, IGroup<GameEntity>>
            _entityGroupDic = new Dictionary<ActorTag, IGroup<GameEntity>>();

        #region Static
        
        #region Access

        public static GameEntity GetEntity(int id)
        {
            return Contexts.game.GetEntityWithIdComp(id);
        }

        public static GameEntity GetPlayer()
        {
            return Instance.Player;
        }

        public static List<GameEntity> GetEntities(ActorTag actorTag,
            ref List<GameEntity> entities)
        {
            if (Instance._entityGroupDic.TryGetValue(actorTag, out var group))
            {
                if (entities == null)
                    entities = new List<GameEntity>();
                else
                    entities.Clear();

                return group.GetEntities(entities);
            }

            return null;
        }
        
        #endregion

        #region Event

        public static void Send(short type)
        {
            Instance._eventDispatcher.Send(type);
        }

        public static void Send<T>(short type, T msgData)
        {
            Instance._eventDispatcher.Send(type, msgData);
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

            _entityGroupDic.Add(ActorTag.Enemy,
                _contexts.game.GetGroup(GameMatcher.EnemyTag));
            _entityGroupDic.Add(ActorTag.Bullet,
                _contexts.game.GetGroup(GameMatcher.BulletTag));
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