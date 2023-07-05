using System.ComponentModel.DataAnnotations;

namespace FoneApi.Model
{
    public partial class Fone_Customer
    {
        public  Fone_Customer()
        {
            FoneApi_Tbl = new HashSet<Fone_Model>();
        }
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Fone_Model> FoneApi_Tbl { get; set; }
    }
}
