

namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlValidationError : Exception
{
    public BlValidationError(string? message) : base(message) { }
}


[Serializable]
public class BlStatusNotFit : Exception
{
    public BlStatusNotFit(string? message) : base(message) { }
}


[Serializable]
public class BlEngineerHasTask : Exception
{
    public BlEngineerHasTask(string? message) : base(message) { }
}

[Serializable]
public class BltaskHasEngineer : Exception
{
    public BltaskHasEngineer(string? message) : base(message) { }
}

