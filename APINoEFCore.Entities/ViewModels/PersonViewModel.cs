namespace APINoEFCore.Entities.ViewModels{
    public class PersonViewModel{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}