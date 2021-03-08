using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class ApplicantModel
    {
        /// <summary>
        /// First Name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Skillset consisting of Frontend, Backend and Database Skillsets
        /// </summary>
        [DataMember]
        public SkillsetModel Skillset { get; set; }

        public ApplicantModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<ApplicantModel>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }
    }
}
