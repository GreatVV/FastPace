using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Type;
    public int Y;
    public int X;

    public void Init(int x, int y,int type)
    {
        X = x;
        Y = y;
        Type = type;
    }

    public event Action<Tile> Clicked;

    public override string ToString()
    {
        return string.Format("({0},{1}) of {2}", X,Y, Type);
    }

    public virtual void InvokeClicked()
    {
        Debug.Log("Clicked "+this);
        Action<Tile> handler = Clicked;
        if (handler != null) handler(this);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            CastRay(Input.GetTouch(0).position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            CastRay(Input.mousePosition);
        }
    }

    private void CastRay(Vector2 position)
    {
        var ray = Camera.main.ScreenPointToRay(position);

        //    Debug.Log("Ray: " + ray);
        Debug.DrawRay(ray.origin, ray.direction*Single.MaxValue);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, Single.MaxValue);
        if (hit.transform == transform)
        {
            InvokeClicked();
        }
    }
}