
namespace DataModel.Common;
public abstract class AuditableEntity
{
/// <summary>
/// Gets or sets a value indicating the time when the object was created.
/// </summary>
/// <remarks>
/// This property is set automatically when DbContext saving changes.
/// </remarks>
public DateTimeOffset Created { get; set; }

/// <summary>
/// Gets or sets a value indicating the time when the object was last modified.
/// </summary>
/// <remarks>
/// This property is set automatically when DbContext saving changes.
/// </remarks>
public DateTimeOffset? LastModified { get; set; }

public virtual DateTimeOffset GetEditedDate() => LastModified ?? Created;
}