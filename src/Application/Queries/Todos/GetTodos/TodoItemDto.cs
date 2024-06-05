using System.ComponentModel.DataAnnotations;
using Assignment.Domain.Enums;

namespace Assignment.Application.TodoLists.Queries.GetTodos;

public class TodoItemDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    [MaxLength(200)]
    public string? Title { get; init; }

    public bool Done { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}
