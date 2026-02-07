namespace crud_v2.ViewModels
{
    public class DeleteEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string? File { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}