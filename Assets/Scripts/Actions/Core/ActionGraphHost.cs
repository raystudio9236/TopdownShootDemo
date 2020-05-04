using System;
using System.Collections.Generic;
using Utils;
using Utils.Pool;

namespace Actions.Core
{
    /// <summary>
    /// 图状态
    /// </summary>
    [Serializable]
    public enum GraphState
    {
        Running,
        Done,
    }

    public class ActionGraphHost : IPoolItem
    {
        /// <summary>
        /// 变量数据背包
        /// </summary>
        public class VarBag
        {
            private const int VarNum = 100;

            private Dictionary<long, int> _iVars;
            private Dictionary<long, float> _fVars;

            private readonly Dictionary<string, object> _graphVars =
                new Dictionary<string, object>();

            public long GetFlag(ActionNode node, int index)
            {
                return node.GetHashCode() * VarNum + index;
            }

            public void Clear()
            {
                _iVars?.Clear();
                _fVars?.Clear();

                _graphVars.Clear();
            }

            #region GraphVar

            public object GetGraphVar(string key)
            {
                if (_graphVars.TryGetValue(key, out var ret))
                    return ret;
                return null;
            }

            public void SetGraphVar(string key, object value)
            {
                if (_graphVars.ContainsKey(key))
                {
                    _graphVars[key] = value;
                }
                else
                {
                    _graphVars.Add(key, value);
                }
            }

            public void RemoveGraphVar(string key)
            {
                if (_graphVars.ContainsKey(key))
                    _graphVars.Remove(key);
            }

            #endregion

            #region Vars

            public VarBag SetInt(long flag, int value)
            {
                return Set(flag, value, ref _iVars);
            }

            public VarBag SetFloat(long flag, float value)
            {
                return Set(flag, value, ref _fVars);
            }

            public int GetInt(long flag)
            {
                return Get(flag, _iVars);
            }

            public float GetFloat(long flag)
            {
                return Get(flag, _fVars);
            }

            public VarBag RemoveInt(long flag)
            {
                return Remove(flag, _iVars);
            }

            public VarBag RemoveFloat(long flag)
            {
                return Remove(flag, _fVars);
            }

            private VarBag Set<T>(long flag,
                T value,
                ref Dictionary<long, T> dic)
            {
                if (dic == null)
                    dic = new Dictionary<long, T>();

                if (dic.ContainsKey(flag))
                    dic[flag] = value;
                else
                    dic.Add(flag, value);

                return this;
            }

            private T Get<T>(long flag, Dictionary<long, T> dic)
            {
                if (dic == null)
                    return default;

                if (dic.TryGetValue(flag, out var ret))
                    return ret;

                return default;
            }

            private VarBag Remove<T>(long flag, Dictionary<long, T> dic)
            {
                if (dic == null)
                    return this;

                if (dic.ContainsKey(flag))
                    dic.Remove(flag);

                return this;
            }

            #endregion
        }

        public string Name { get; private set; }

        public ActionGraph Graph { get; private set; }
        public GameEntity Entity { get; private set; }

        public GraphState State { get; private set; }
        public VarBag Var { get; } = new VarBag();

        private readonly List<ActionNode> _executeNodeList =
            new List<ActionNode>();

        private readonly HashSet<ActionNode> _executeNodeSet =
            new HashSet<ActionNode>();

        public void AwakeFromPool()
        {
        }

        public void RecycleToPool()
        {
            Name = string.Empty;

            Graph = null;
            Entity = null;

            Var.Clear();

            _executeNodeList.Clear();
            _executeNodeSet.Clear();
        }

        public void SetData(string name, ActionGraph graph, GameEntity entity)
        {
            Name = name;
            Graph = graph;
            Entity = entity;

            if (graph.UpdateNode != null)
            {
                _executeNodeList.Add(graph.UpdateNode);
                _executeNodeSet.Add(graph.UpdateNode);
            }

            if (graph.EntryNode != null)
            {
                _executeNodeList.Add(graph.EntryNode);
                _executeNodeSet.Add(graph.EntryNode);
            }

            State = GraphState.Running;
        }

        public void RegisterExecute(ActionNode node)
        {
            if (_executeNodeSet.Contains(node))
                return;

            _executeNodeList.Add(node);
            _executeNodeSet.Add(node);
        }

        public void Execute()
        {
            Graph.Host = this;

            switch (State)
            {
                case GraphState.Running:
                {
                    for (var i = _executeNodeList.Count - 1; i >= 0; i--)
                    {
                        var node = _executeNodeList[i];
                        if (node.HasExecuted || !node.Execute()) continue;

                        _executeNodeSet.Remove(node);
                        _executeNodeList.RemoveAt(i);
                    }

                    if (_executeNodeList.Count == 0)
                    {
                        State = GraphState.Done;
                    }
                    else
                    {
                        foreach (var actionNode in _executeNodeList)
                            actionNode.HasExecuted = false;
                    }

                    break;
                }

                case GraphState.Done:
                {
                    break;
                }
            }

            Graph.Host = null;
        }
    }
}