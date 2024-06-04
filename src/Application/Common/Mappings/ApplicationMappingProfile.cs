using Assignment.Application.TodoLists.Queries.GetTodos;
using Assignment.Domain.Entities;

namespace Assignment.Application.Common.Mappings;
internal class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<TodoList, TodoListDto>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}
