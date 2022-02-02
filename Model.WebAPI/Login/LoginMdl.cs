using System;
using System.ComponentModel.DataAnnotations;

namespace Model.WebAPI.Login
{
    public class LoginMdl
    {
        [Required(ErrorMessage = "The Email Address is required.")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Guid { get; set; }

        public string RememberKey { get; set; }
        public string EmailAddressKey { get; set; }

        public int ResponseType { get; set; }
        public string Message { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public string CurrentLatitude { get; set; }
        public string CurrentLongitude { get; set; }
        public string LocalDateTime { get; set; }
        public string LocalDateTimeZoneDiff { get; set; }
        public string SessionId { get; set; }
        public bool AccountConfirmed { get; set; }
        public DateTime AccountExpiry { get; set; }
    }
}
