namespace Authentication.Domain.User.Validators
{
    public interface IMailAddressValidator
    {
        bool Validate(string mailAddress);
    }
}
