using Microsoft.AspNetCore.Identity;

namespace FoneApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string TenantId { get; set; }
        public int EmpId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    }
}
