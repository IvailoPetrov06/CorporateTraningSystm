namespace CorporateTraningSystm.View
{
    public class EnrollmentView
    {
        public int EnrollmentId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeView Employee { get; set; }
        public int CourseId { get; set; }
        public CourseView Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string CompletionStatus { get; set; }
    }
}
