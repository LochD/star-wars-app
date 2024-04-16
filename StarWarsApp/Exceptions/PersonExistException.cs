namespace StarWarsApp.Exceptions;

public class PersonExistException : Exception
{
    public PersonExistException(string message) : base(message)
    {
    }
}