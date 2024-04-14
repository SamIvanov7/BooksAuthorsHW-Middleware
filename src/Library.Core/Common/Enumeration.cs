using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Library.Core.Common;

/// <summary>
/// See https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
/// </summary>
public abstract class Enumeration<TEnumeration, TValue>(TValue id, string name)
    : IEnumeration, IComparable<TEnumeration>, IEquatable<TEnumeration>
    where TEnumeration : Enumeration<TEnumeration, TValue>
    where TValue : IComparable
{
    private static readonly TEnumeration[] Enumerations = GetEnumerations();

    public TValue Id { get; private set; } = id;

    public string Name { get; private set; } = name;

    public static TEnumeration[] GetAll() => Enumerations;

    public static TEnumeration FromValue(TValue value) =>
        Parse(value, "value", item => item.Id.Equals(value));

    public static TEnumeration Parse(string? displayName) =>
        Parse(displayName, "display name", item => item.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase));

    public static bool TryParse(TValue value, out TEnumeration? result) =>
        TryParse(x => x.Id.Equals(value), out result);

    public static bool TryParse(string displayName, out TEnumeration? result) =>
        TryParse(x => x.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase), out result);

    public int CompareTo(TEnumeration? other) => other == null ? 1 : Id.CompareTo(other.Id);

    public sealed override string ToString() => Name;

    public override bool Equals(object? obj) => Equals(obj as TEnumeration);

    public bool Equals(TEnumeration? other) => other != null && Id.Equals(other.Id);

    public override int GetHashCode() => HashCode.Combine(Id);

    public static Enumeration<TEnumeration, TValue>? ToBaseEnumeration(TValue value) => FromValue(value);

    public TValue ToTValue() => Id;

    public static explicit operator Enumeration<TEnumeration, TValue>(TValue value) => FromValue(value);

    public static implicit operator TValue([DisallowNull] Enumeration<TEnumeration, TValue> enumeration) => enumeration.Id;

    public static implicit operator string(Enumeration<TEnumeration, TValue> enumeration) => enumeration.Name;

    public static bool operator ==(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right) => !(left == right);

    public static bool operator <(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right) =>
        left is null ? right is not null : left.CompareTo((TEnumeration?)right) < 0;

    public static bool operator <=(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right) =>
        left is null || left.CompareTo((TEnumeration?)right) <= 0;

    public static bool operator >(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right) =>
        left is not null && left.CompareTo((TEnumeration?)right) > 0;

    public static bool operator >=(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right) =>
        left is null ? right is null : left.CompareTo((TEnumeration?)right) >= 0;

    private static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration? result)
    {
        result = GetAll().FirstOrDefault(predicate);
        return result != null;
    }

    private static TEnumeration[] GetEnumerations()
    {
        var enumerationType = typeof(TEnumeration);
        return enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
            .Select(info => info.GetValue(null))
            .Cast<TEnumeration>()
            .ToArray();
    }

    private static TEnumeration Parse(object? value, string description, Func<TEnumeration, bool> predicate)
    {
        if (!TryParse(predicate, out var result))
        {
            var message = $"'{value}' is not a valid {description} in {typeof(TEnumeration)}";
            throw new ArgumentException(message, nameof(value));
        }

        return result!;
    }
}