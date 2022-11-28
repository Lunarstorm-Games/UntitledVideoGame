using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeVincent
{
    public abstract class BehaviourTree
    {
        private Node _root = null;

        public void Initialize()
        {
            _root = SetupTree();
        }

        public void Evaluate()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract Node SetupTree();


    } 
}


