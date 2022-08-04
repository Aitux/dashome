namespace Dashome.Core.Models;

public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
{
    public static Task Task => Task.FromResult(_value);
    public bool Equals(Unit other) => true;

    public int CompareTo(Unit other) => 0;

    private static readonly Unit _value = new();
    public static ref readonly Unit Value => ref _value;

    public int CompareTo(object obj) => 0;
    public override bool Equals(object obj) => obj is Unit;
    public override int GetHashCode() => 0;
    public override string ToString() => "()";
}
