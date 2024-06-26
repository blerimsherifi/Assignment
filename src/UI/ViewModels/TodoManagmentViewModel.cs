﻿using System.Windows.Input;
using Assignment.Application.Common.Cashing;
using Assignment.Domain.Enums;
using Caliburn.Micro;
using MediatR;

namespace Assignment.UI;
internal class TodoManagmentViewModel : Screen
{
    private readonly ISender _sender;
    private readonly IWindowManager _windowManager;
    private readonly SimpleCache<int, IList<assignmentDTOs.TodoListDto>> _cache;

    private IList<assignmentDTOs.TodoListDto> todoLists;
    public IList<assignmentDTOs.TodoListDto> TodoLists
    {
        get
        {
            return todoLists;
        }
        set
        {
            todoLists = value;
            NotifyOfPropertyChange(() => TodoLists);
        }
    }

    private assignmentDTOs.TodoListDto _selectedTodoList;
    public assignmentDTOs.TodoListDto SelectedTodoList
    {
        get => _selectedTodoList;
        set
        {
            _selectedTodoList = value;
            NotifyOfPropertyChange(() => SelectedTodoList);
        }
    }

    private assignmentDTOs.TodoItemDto _selectedItem;
    public assignmentDTOs.TodoItemDto SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            NotifyOfPropertyChange(() => SelectedItem);
        }
    }

    public ICommand AddTodoListCommand { get; private set; }
    public ICommand AddTodoItemCommand { get; private set; }
    public ICommand DoneTodoItemCommand { get; private set; }

    public TodoManagmentViewModel(ISender sender, IWindowManager windowManager)
    {
        _sender = sender;
        _windowManager = windowManager;
        _cache = SimpleCache<int, IList<assignmentDTOs.TodoListDto>>.Instance;
        Initialize();
    }

    private async void Initialize()
    {
        await RefereshTodoLists();

        AddTodoListCommand = new RelayCommand(AddTodoList);
        AddTodoItemCommand = new RelayCommand(AddTodoItem);
        DoneTodoItemCommand = new RelayCommand(DoneTodoItem);
    }

    private async Task RefereshTodoLists(bool getFromSource = false)
    {
        var selectedListId = SelectedTodoList?.Id;

        // Check if the value is present in the cache
        var cachedValue = _cache.Get((int)CachedKeys.TodoLists);

        if (cachedValue != null && !getFromSource)
        {
            TodoLists = cachedValue;
        }
        else
        {
            // Value not found in the cache, fetch it from the sender
            TodoLists = await _sender.Send(new assignmentQueries.GetTodos.GetTodosQuery());

            // Store the fetched value in the cache
            _cache.Set((int)CachedKeys.TodoLists, TodoLists);
        }

        if (selectedListId.HasValue && selectedListId.Value > 0)
        {
            SelectedTodoList = TodoLists.FirstOrDefault(list => list.Id == selectedListId.Value);
        }
    }

    private async void AddTodoList(object obj)
    {
        var todoList = new TodoListViewModel(_sender);
        await _windowManager.ShowDialogAsync(todoList);
        await RefereshTodoLists(getFromSource: true);
    }

    private async void AddTodoItem(object obj)
    {
        if (SelectedTodoList == null)
        {
            return;
        }

        var todoItem = new TodoItemViewModel(_sender, SelectedTodoList.Id);
        await _windowManager.ShowDialogAsync(todoItem);
        await RefereshTodoLists(getFromSource: true);
    }

    private async void DoneTodoItem(object obj)
    {
        if (CanItemBeMarkedAsDone())
        {
            await _sender.Send(new assignmentCommands.DoneTodoItem.DoneTodoItemCommand(SelectedItem.Id));
            await RefereshTodoLists(getFromSource: true);
        }
    }

    private bool CanItemBeMarkedAsDone()
    {
        return SelectedItem != null && !SelectedItem.Done;
    }
}
