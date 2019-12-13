using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UserValidation.Strings;

namespace UserValidation.Models
{
    public class User : ValidationBase
    {
        private string _nickName;
        private string _email;
        private string _firstName;
        private string _lastname;
        private string _phone;
        private string _plainPassword;

        [JsonProperty("id")] public int Id { get; }


        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "NotNullErrorText")]
        [JsonProperty("nickname")]
        public string NickName
        {
            get => _nickName;
            set
            {
                ValidateProperty(value);
                SetProperty(ref _nickName, value);
            }
        }

        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "NotNullErrorText")]
        [EmailAddress(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "EmailErrorText")]
        [JsonProperty("email")]
        public string Email
        {
            get => _email;
            set
            {
                ValidateProperty(value);
                SetProperty(ref _email, value);
            }
        }

        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "NotNullErrorText")]
        [JsonProperty("first_name")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                ValidateProperty(value);
                SetProperty(ref _firstName, value);
            }
        }

        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "NotNullErrorText")]
        [JsonProperty("last_name")]
        public string LastName
        {
            get => _lastname;
            set
            {
                ValidateProperty(value);
                SetProperty(ref _lastname, value);
            }
        }

        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "NotNullErrorText")]
        [MinLength(7, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "PasswordErrorText")]
        [JsonProperty("plain_password")]
        public string PlainPassword
        {
            get => _plainPassword;
            set
            {
                ValidateProperty(value);
                SetProperty(ref _plainPassword, value);
            }
        }

        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "NotNullErrorText")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", 
            ErrorMessageResourceType = typeof(AppResources), 
            ErrorMessageResourceName = "PhoneErrorText")]
        [JsonProperty("phone")]
        public string Phone
        {
            get => _phone;
            set
            {
                ValidateProperty(value);
                SetProperty(ref _phone, value);
            }
        }

        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);
        }
    }
}
