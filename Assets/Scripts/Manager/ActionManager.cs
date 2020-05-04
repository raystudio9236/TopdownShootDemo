using System.Collections.Generic;
using Actions.Core;
using Other;
using UnityEngine;
using Utils;

namespace Manager
{
    public class ActionManager : MonoBehaviour
    {
        private const string PATH_PREFIX = "ActionGraphs";

        public static ActionManager Instance;

        private InstancePool<ActionGraphHost> _actionPool =
            new InstancePool<ActionGraphHost>();

        private Dictionary<ActionTag, ActionGraph> _graphDic =
            new Dictionary<ActionTag, ActionGraph>();

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }

        public ActionGraphHost GetGraph(ActionTag actionTag, GameEntity entity)
        {
            if (!_graphDic.TryGetValue(actionTag, out var graph))
            {
                graph = Resources.Load<ActionGraph>(
                    $"{PATH_PREFIX}/{actionTag.ToString()}");
                if (graph == null)
                {
                    Debug.LogError(
                        $"找不到名为{actionTag.ToString()}的ActionGraph资源");
                    return null;
                }

                _graphDic.Add(actionTag, graph);
            }

            var host = _actionPool.Get();
            host.SetData(graph, entity);
            return host;
        }

        public void RecycleGraph(ActionGraphHost host)
        {
            _actionPool.Recycle(host);
        }
    }
}