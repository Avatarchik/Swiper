using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AraxisTools
{

    public delegate void VoidAction();

    public delegate void VoidAction<in T>(params T[] parameters);

    public delegate bool BoolAction();

    public delegate bool BoolAction<in T>(params T[] parameters);

    public delegate int IntAction();

    public delegate int IntAction<in T>(params T[] parameters);

    public delegate long LongAction();

    public delegate long LongAction<in T>(params T[] parameters);

    public delegate float FloatAction();

    public delegate float FloatAction<in T>(params T[] parameters);

    public delegate double DoubleAction();

    public delegate double DoubleAction<in T>(params T[] parameters);

    public delegate string StringAction();

    public delegate string StringAction<in T>(params T[] parameters);

    public delegate IEnumerable<T> EnumerableAction<T>();

    public delegate IEnumerable<T1> EnumerableAction<T1, in T2>(params T2[] parameters);

    public delegate T GenericAction<out T>();

    public delegate T1 GenericAction<out T1, in T2>(params T2[] parameters);

}