using Raiqub.Generators.InterpolationCodeWriter.Collections;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests.Collections;

public class EquatableArrayTest
{
    [Fact]
    public void PrimitiveComparison()
    {
        int[] val1 = [1, 2, 3, 4, 5];
        int[] val2 = [1, 2, 3, 4, 5];

        var arr1 = val1.ToEquatableArray();
        var arr2 = val2.ToEquatableArray();

        Assert.True(arr1.Equals(arr2));
        Assert.True(arr1.GetHashCode() == arr2.GetHashCode());
    }

    [Fact]
    public void RecordComparison()
    {
        Record[] val1 = [new(1), new(2), new(3), new(4), new(5)];
        Record[] val2 = [new(1), new(2), new(3), new(4), new(5)];

        var arr1 = val1.ToEquatableArray();
        var arr2 = val2.ToEquatableArray();

        Assert.True(arr1.Equals(arr2));
        Assert.True(arr1.GetHashCode() == arr2.GetHashCode());
    }

    [Fact]
    public void NestedEquatableArrayComparison()
    {
        EquatableArray<int>[] val1 = [new([1]), new([2]), new([3]), new([4]), new([5])];
        EquatableArray<int>[] val2 = [new([1]), new([2]), new([3]), new([4]), new([5])];

        var arr1 = val1.ToEquatableArray();
        var arr2 = val2.ToEquatableArray();

        Assert.True(arr1.Equals(arr2));
        Assert.True(arr1.GetHashCode() == arr2.GetHashCode());
    }

    [Fact]
    public void BoxedNestedEquatableArrayComparison()
    {
        EquatableArray<int>[] val1 = [new([1]), new([2]), new([3]), new([4]), new([5])];
        EquatableArray<int>[] val2 = [new([1]), new([2]), new([3]), new([4]), new([5])];

        object arr1 = val1.ToEquatableArray();
        var arr2 = val2.ToEquatableArray();

        Assert.True(arr1.Equals(arr2));
        Assert.True(arr2.Equals(arr1));
        Assert.True(arr1.GetHashCode() == arr2.GetHashCode());
    }

    public sealed record Record(int Value);
}
