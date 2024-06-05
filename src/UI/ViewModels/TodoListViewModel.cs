using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Assignment.Application.TodoLists.Commands.CreateTodoList;
using Assignment.Application.TodoLists.Queries.CheckTodoTitle;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using MediatR;

namespace Assignment.UI;
public class TodoListViewModel : Screen
{
    private readonly ISender _sender;
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;

    private string _title;

    [MaxLength(200)]
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            NotifyOfPropertyChange(() => Title);
        }
    }

    public ISnackbarMessageQueue SnackbarMessageQueue
    {
        get => _snackbarMessageQueue;
    }

    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }

    public TodoListViewModel(ISender sender)
    {
        _sender = sender;

        SaveCommand = new RelayCommand(SaveExecute);
        CloseCommand = new RelayCommand(CloseExecute);
        _snackbarMessageQueue = new SnackbarMessageQueue();
    }

    private async void SaveExecute(object parameter)
    {

        if (await CheckIfTitleExist())
        {
            //MessageBox.Show("Title already exist");
            _snackbarMessageQueue.Enqueue("Title already exist");
            return;
        }

        await _sender.Send(new CreateTodoListCommand(Title));
        await TryCloseAsync(true);
    }

    private async Task<bool> CheckIfTitleExist()
    {
        return await _sender.Send(new CheckTodoTitleQuery { Title = Title });
    }

    private async void CloseExecute(object parameter)
    {
        await TryCloseAsync(false);
    }
}
