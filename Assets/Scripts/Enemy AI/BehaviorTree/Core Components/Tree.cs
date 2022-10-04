using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree
    {
        private Node _root = null;

        public void Initialize()
        {
            _root = SetupTree();
        }

        public void Start()
        {
            if (_root != null)
                _root.Start();
        }

        public void Evaluate()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract Node SetupTree();


    }

}
