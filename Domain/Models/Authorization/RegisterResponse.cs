namespace net_design_pattern.Domain.Models.Authorization
{
    public class RegisterResponse
    {
        public RegisterResponse()
        {
            IsSuccess = true;
        }
        public int AccountId {get; set;}
        public bool IsSuccess {get; set;}
        public string Error {get; set;}
    }
}