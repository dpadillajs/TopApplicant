using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class SkillsetModel
    {
        /// <summary>
        /// Frontend Skillset
        /// </summary>
        [DataMember]
        public FrontendSkillsetModel FrontendSkillset { get; set; }

        /// <summary>
        /// Backend Skillset
        /// </summary>
        [DataMember]
        public BackendSkillsetModel BackendSkillset { get; set; }

        /// <summary>
        /// Database Skillset
        /// </summary>
        [DataMember]
        public DatabaseSkillsetModel DatabaseSkillset { get; set; }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<SkillsetModel>
        {
            public Validator()
            {
            }
        }
    }
}
