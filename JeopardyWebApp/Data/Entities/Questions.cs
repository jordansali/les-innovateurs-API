namespace JeopardyWebApp.Data.Entities
{
    public class Questions
    {
        public Categories Category { get; set; }      // not sure if required, based on entities' relationships  

        // Required attributes
        public int Id { get; set; }
        public string QuestionEn { get; set; }
        public string AnswerFr { get; set; }
        public int? Points { get; set; }
        public int? TimeLimit { get; set; }        

        // Optional attributes
        public string QuestionFr { get; set; }
        public string AnswerEn { get; set; }                
        public string Hint { get; set; }
    }
}
