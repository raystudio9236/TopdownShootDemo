using System.Collections.Generic;
using Actions.Core;
using Other;
using UnityEngine;
using Utils;
using Utils.Pool;

namespace Manager
{
    public class ActionManager : MonoBehaviour
    {
        private const string PATH_PREFIX = "ActionGraphs";

        public static ActionManager Instance;

        private InstancePool<ActionGraphHost> _actionPool =
            new InstancePool<ActionGraphHost>();

        private Dictionary<string, ActionGraph> _graphDic =
            new Dictionary<string, ActionGraph>();

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }

        public void AddGraph(GameEntity entity, ActionTag actionTag)
        {
            AddGraph(entity, actionTag.ToString());
        }

        public void AddGraph(GameEntity entity, string graphName)
        {
            if (!entity.hasActionComp)
                entity.AddActionComp(new List<ActionGraphHost>());

            entity.actionComp.ActionGraphHostArr.Add(
                GetGraph(graphName, entity));
        }

        public void RemoveGraph(GameEntity entity, ActionTag actionTag)
        {
            RemoveGraph(entity, actionTag.ToString());
        }

        public void RemoveGraph(GameEntity entity, string graphName)
        {
            if (!entity.hasActionComp)
                return;

            var list = entity.actionComp.ActionGraphHostArr;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Name == graphName)
                    list.RemoveAt(i);
            }
        }

        public ActionGraphHost GetGraph(ActionTag actionTag, GameEntity entity)
        {
            return GetGraph(actionTag.ToString(), entity);
        }

        public ActionGraphHost GetGraph(string graphName, GameEntity entity)
        {
            if (!_graphDic.TryGetValue(graphName, out var graph))
            {
                graph = Resources.Load<ActionGraph>(
                    $"{PATH_PREFIX}/{graphName.ToString()}");
                if (graph == null)
                {
                    Debug.LogError(
                        $"找不到名为{graphName}的ActionGraph资源");
                    return null;
                }

                _graphDic.Add(graphName, graph);
            }

            var host = _actionPool.Get();
            host.SetData(graphName, graph, entity);
            return host;
        }

        public void RecycleGraph(ActionGraphHost host)
        {
            _actionPool.Recycle(host);
        }
    }
}