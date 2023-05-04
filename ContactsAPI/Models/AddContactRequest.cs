namespace ContactsAPI.Models
{
    public class AddContactRequest
    {
        public string FullName { get; set; }
        public string LastName { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}