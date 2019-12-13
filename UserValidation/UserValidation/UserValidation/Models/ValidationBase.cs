using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Prism.Mvvm;

namespace UserValidation.Models
{
    public class ValidationBase : BindableBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public Dictionary<string, string> Errors
        {
            get => _errors;
            set => SetProperty(ref value, _errors);
        }

        /// <summary>
        /// Will be handy if we want to display a list of errors (no dictionary)
        /// </summary>
        /// <param name="propertyName">the name of the property</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (Errors.ContainsKey(propertyName) && (Errors[propertyName].Any()))
                {
                    return Errors[propertyName].ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                return Errors.SelectMany(err => err.Value.ToList()).ToList();
            }
        }

        /// <summary>
        /// Check if there is any error in the errors list
        /// </summary>
        public bool HasErrors
        {
            get { return Errors.Any(propErrors => propErrors.Value.Any()); }
        }

        /// <summary>
        /// Notify the view that errors changed
        /// </summary>
        private void RaiseErrorChanged()
        {
            RaisePropertyChanged("Errors");
            RaisePropertyChanged("HasErrors");
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Validates a property from validation rules set on entity
        /// </summary>
        /// <param name="value">The value of the property</param>
        /// <param name="propertyName">The name of the property</param>
        protected virtual void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            var validationContext = new ValidationContext(this, null)
            {
                MemberName = propertyName
            };

            var validationResults = new List<ValidationResult>();
   
            Validator.TryValidateProperty(value, validationContext, validationResults);

            RemoveErrorsByPropertyName(propertyName);

            HandleValidationResults(validationResults);
        }

        /// <summary>
        /// Removes an error from the Errors dictionary based on property name
        /// </summary>
        /// <param name="propertyName">the name of the property</param>
        private void RemoveErrorsByPropertyName(string propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                Errors.Remove(propertyName);
            }
        }

        /// <summary>
        /// Extract from ValidationResult the property name and the error message associated
        /// </summary>
        /// <param name="validationResults"></param>
        private void HandleValidationResults(IEnumerable<ValidationResult> validationResults)
        {

            var resultsByPropertyName = from results in validationResults
                from memberNames in results.MemberNames
                group results by memberNames
                into groups
                select groups;

            foreach (var property in resultsByPropertyName)
            {
                Errors.Add(property.Key, property.First().ErrorMessage);
            }

            RaiseErrorChanged();
        }
    }
}
