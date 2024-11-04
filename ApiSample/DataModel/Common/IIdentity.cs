namespace DataModel.Common;

public interface IIdentity<out T>
{
    T Id { get; }
}