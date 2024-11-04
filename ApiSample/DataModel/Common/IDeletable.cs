
namespace DataModel.Common;


/// <summary>
/// Marks an entity as having the ability to be soft-deleted.
/// </summary>
public interface IDeletable
{
    /// <summary>
    /// Gets or sets a value indicating whether the object is “deleted” or inactive.
    /// </summary>
    /// <remarks>
    /// This property is set automatically when saving changes if the entry is in the Deleted state.
    /// </remarks>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the time when the object was deleted.
    /// </summary>
    /// <remarks>
    /// This property is set automatically when DbContext saving changes.
    /// </remarks>
    DateTimeOffset? Deleted { get; set; }
}