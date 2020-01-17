namespace DevEnvAzure.DataContracts
{
    public class TaskSp
    {
        public TaskSp()
        {
            this.__metadata = new Metadata();
            //this.__metadata.type = "SP.Data.Read_x0020_and_x0020_Sign_x0020_ConfirmationListItem";
        }

        public Metadata __metadata { get; set; }
        public int Id { get; set; }
        public int PercentComplete { get; set; }
        public string Checkmark { get; set; }
    }
}
