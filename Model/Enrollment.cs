namespace CorporateTraningSystm.Model
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string CompletionStatus { get; set; }
    }
}
