﻿
namespace BO;
/// <summary>
/// class of Taskilist
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public string? Description { get; set; } = null;
    public string? Alias { get; set; } = null;
    public Status? Status { get; set; } = null;

    public override string ToString()
    { return this.ToStringProperty(); }
}
