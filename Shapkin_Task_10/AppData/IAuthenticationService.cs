namespace Shapkin_Task_10.AppData
{
    public interface IAuthenticationService
    {
        bool ValidateUser(string username, string password, out bool isAdmin);
    }
}
