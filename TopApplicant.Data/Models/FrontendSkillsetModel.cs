using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class FrontendSkillsetModel
    {
        /// <summary>
        /// HTML
        /// </summary>
        [DataMember]
        public bool HTML { get; set; }

        /// <summary>
        /// CSS
        /// </summary>
        [DataMember]
        public bool CSS { get; set; }

        /// <summary>
        /// JavaScript
        /// </summary>
        [DataMember]
        public bool JavaScript { get; set; }

        /// <summary>
        /// TypeScript
        /// </summary>
        [DataMember]
        public bool TypeScript { get; set; }

        /// <summary>
        /// jQuery
        /// </summary>
        [DataMember]
        public bool jQuery { get; set; }

        /// <summary>
        /// AngularJS
        /// </summary>
        [DataMember]
        public bool AngularJS { get; set; }

        /// <summary>
        /// Vue
        /// </summary>
        [DataMember]
        public bool Vue { get; set; }

        /// <summary>
        /// React
        /// </summary>
        [DataMember]
        public bool React { get; set; }

        /// <summary>
        /// Angular
        /// </summary>
        [DataMember]
        public bool Angular { get; set; }

        /// <summary>
        /// Python
        /// </summary>
        [DataMember]
        public bool Python { get; set; }

        /// <summary>
        /// Django
        /// </summary>
        [DataMember]
        public bool Django { get; set; }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<FrontendSkillsetModel>
        {
            public Validator()
            {
            }
        }
    }
}
