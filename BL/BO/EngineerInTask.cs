﻿
namespace BO;
/// <summary>
/// class EngineerInTask
/// </summary>
public class EngineerInTask
{
    public int Id { get; init; }
    public string? Name { get; set; }

    public override string ToString()
    { return this.ToStringProperty(); }
}
