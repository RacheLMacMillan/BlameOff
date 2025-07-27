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

public interface IJumpable<T0, T1, T2>
{
    public Vector3 Jump(T0 t0, T1 t1, T2 t2);
}