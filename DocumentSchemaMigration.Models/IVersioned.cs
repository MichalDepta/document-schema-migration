namespace DocumentSchemaMigration.Models
{
    public interface IVersioned
    {
        int Version { get; }
    }
}