using System.ComponentModel.DataAnnotations;

namespace QA.Dtos
{
    public class LoginData
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
