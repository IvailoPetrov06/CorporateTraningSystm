namespace CorporateTraningSystm.Model
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
