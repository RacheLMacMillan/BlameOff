using UnityEngine;

public interface IJumpable<T0>
{
    public Vector3 Jump(T0 t0
    );
}

public interface IJumpable<T0, T1>
{
    public Vector3 Jump(T0 t0, T1 t1);
}