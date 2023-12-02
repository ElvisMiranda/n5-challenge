namespace N5.Challenge.Domain.Abstractions;

public abstract class Entity(int id)
{
    public int Id { get; } = id;
}