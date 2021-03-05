namespace TopApplicant.Data.Models
{
    public class Applicant
    {
        public string FirstName;

        public string LastName;

        public Skillset Skillset;

        public Applicant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
