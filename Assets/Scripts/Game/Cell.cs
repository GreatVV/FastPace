using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Pair
    {
        public Cell Neigbour;
        public Vector3 OffsetRelativeCenter;
        public Cell Current;

        public Vector3 WorldPosition
        {
            get { return Current.transform.position + OffsetRelativeCenter; }
        }

        public override string ToString()
        {
            return string.Format("Pair: {0} for {1} insert to {2}", OffsetRelativeCenter, Current, Neigbour);
        }
    }

    public class Cell : MonoBehaviour
    {
        public List<Pair> Neighbours = new List<Pair>();
        public int Type;

        public event Action<Cell> Clicked;

        public override string ToString()
        {
            return string.Format("({0},{1}) of {2}", transform.position.x, transform.position.y, Type);
        }

        public virtual void InvokeClicked()
        {
            Debug.Log("Clicked " + this);
            Action<Cell> handler = Clicked;
            if (handler != null) handler(this);
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                CastRay(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
            }

            if (Input.GetMouseButtonDown(0))
            {
                CastRay(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        private void CastRay(Vector2 position)
        {
            if (collider2D.OverlapPoint(position))
            {
                InvokeClicked();
            }
        }

        void Awake()
        {
            foreach (var neighbour in Neighbours)
            {
                neighbour.Current = this;
            }
        }

        private void OnDrawGizmos()
        {
            foreach (Pair neighbour in Neighbours)
            {
                Vector3 pos = transform.position + neighbour.OffsetRelativeCenter;
                Gizmos.DrawLine(transform.position, pos);
                
                Gizmos.color = neighbour.Neigbour == null ? Color.white : Color.red;
                Gizmos.DrawWireSphere(pos, 0.5f);
                Gizmos.color = Color.white;
            }
        }
    }
}