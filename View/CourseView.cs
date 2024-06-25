namespace CorporateTraningSystm.View
{
    public class CourseView
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int TrainerId { get; set; }
        public TrainerView Trainer { get; set; }
    }
}
