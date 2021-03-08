using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class DatabaseSkillsetModel
    {
        /// <summary>
        /// MongoDB
        /// </summary>
        [DataMember]
        public bool MongoDB { get; set; }

        /// <summary>
        /// Microsoft SQL Server
        /// </summary>
        [DataMember]
        public bool MSSQL { get; set; }

        /// <summary>
        /// Transact-SQL
        /// </summary>
        [DataMember]
        public bool TransactSQL { get; set; }

        /// <summary>
        /// Postgres SQL
        /// </summary>
        [DataMember]
        public bool PostgresSQL { get; set; }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<DatabaseSkillsetModel>
        {
            public Validator()
            {
            }
        }
    }
}
