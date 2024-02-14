

namespace BO;
/// <summary>
/// Exception thrown when an entity does not exist in the  (BL) layer.
/// </summary>
[Serializable]

public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when attempting to create an entity that already exists in the BL layer.
/// </summary>
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when deletion of an entity is not possible in the BL layer.
/// </summary>
[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when there is a validation error in the BL layer.
/// </summary>
[Serializable]
public class BlValidationError : Exception
{
    public BlValidationError(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when the status of an entity does not fit the expected criteria in the BL layer.
/// </summary>
[Serializable]
public class BlStatusNotFit : Exception
{
    public BlStatusNotFit(string? message) : base(message) { }
}

/// <summary>
///  Exception thrown when an engineer has an associated task in the BL layer.
/// </summary>
[Serializable]
public class BlEngineerHasTask : Exception
{
    public BlEngineerHasTask(string? message) : base(message) { }
}


/// <summary>
/// Exception thrown when a task has an associated engineer in the BL layer.
/// </summary>
[Serializable]
public class BltaskHasEngineer : Exception
{
    public BltaskHasEngineer(string? message) : base(message) { }
}

