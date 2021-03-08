using System;
using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class BackendSkillsetModel
    {
        /// <summary>
        /// Node
        /// </summary>
        [DataMember]
        public bool Node { get; set; }

        /// <summary>
        /// C#
        /// </summary>
        [DataMember]
        public bool CSharp { get; set; }

        /// <summary>
        /// Java
        /// </summary>
        [DataMember]
        public bool Java { get; set; }


        /// <summary>
        /// GoLang
        /// </summary>
        [DataMember]
        public bool GoLang { get; set; }


        /// <summary>
        /// Spring Framework
        /// </summary>
        [DataMember]
        public bool SpringFramework { get; set; }


        /// <summary>
        /// .NET Core
        /// </summary>
        [DataMember]
        public bool DotNetCore { get; set; }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<BackendSkillsetModel>
        {
            public Validator()
            {
            }
        }
    }
}
