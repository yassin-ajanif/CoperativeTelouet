namespace CoperativeTelouet.UI.Services;

public interface IUserDialogService
{
    Task ShowSuccessAsync(string message);
    Task ShowErrorAsync(string message);
}
