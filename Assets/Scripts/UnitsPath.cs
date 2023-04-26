using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsPath : MonoBehaviour
{
    public Transform[] points;
    public List<Unit> units = new List<Unit>();

    public static UnitsPath Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetPoint(float t)
    {
        return Multilerp.MultilerpFunction(points.Select(v => v.position).ToArray(), t);
    }
}
