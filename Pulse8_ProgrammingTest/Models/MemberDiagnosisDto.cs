
namespace Pulse8_ProgrammingTest.Models {
    /// <summary>
    /// Class to hold the data about member diagnosis and severity levels returned from the database 
    /// </summary>
    public class MemberDiagnosisDto {
        /*
         
        Creating a generic class for simplicity
        Ideally, this would be broken down into separate classes, each one mirrored against a db table
        and the result of a query would appropriately instanciate objects as needed
         
         */

        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? MostSevereDiagnosisId { get; set; }
        public string MostSevereDiagnosisDescription { get; set; }
        public int DiagnosisCategoryId { get; set; }
        public string DiagnosisCategoryDescription { get; set; }
        public int DiagnosisCategoryScore { get; set; }
        public bool IsMostSevere { get; set; }

        public override string ToString() {
            return $@"Information for Member ID: {this.MemberId} --
                Name: {FirstName} {LastName};
                {(IsMostSevere ? "Most severe diagnosis category ID: " + MostSevereDiagnosisId + ", Description: " + MostSevereDiagnosisDescription : "")}
                Diagnosis Category ID: {DiagnosisCategoryId}, Diagnosis Category Description: {DiagnosisCategoryDescription}, Diagnosis Category Score: {DiagnosisCategoryScore}";
        }

    }
}
