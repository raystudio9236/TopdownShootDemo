using UnityEngine;
using XNode;

namespace Actions.Core
{
    [NodeHideInCreateMenu]
    public class ActionNode : Node
    {
        protected ActionGraph parent => graph as ActionGraph;
        protected ActionGraphHost host => parent.Host;
        protected GameEntity entity => parent.Entity;
        protected internal bool HasExecuted = false; // 标记这一帧有没有执行过
        
        public bool Enter()
        {
            OnEnter();

            return Execute();
        }

        public void Exit()
        {
            OnExit();
        }

        public bool Execute()
        {
            HasExecuted = false;

            var ret = OnExecute();
            if (!ret)
                parent.Host.RegisterExecute(this);
            
            HasExecuted = true;
            
            return ret;
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected virtual bool OnExecute()
        {
            return true;
        }

        protected bool ExitNode(string portName)
        {
            var exitPort = GetOutputPort(portName);

            if (exitPort == null)
            {
                Debug.LogError($"{this.GetType()} Exit port not found");
                return true;
            }
            
            Exit();

            if (!exitPort.IsConnected)
                return true;

            // 出口连接的节点
            var node = exitPort.Connection.node as ActionNode;
            return node.Enter();
        }
    }
}