using System;

namespace Marketplace.SaaS.Accelerator.DataAccess.DataModel
{
    public class Scur_User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int? AddressId { get; set; }
        public string Email { get; set; }        
        public int GenderType { get; set; }        
        public bool IsSiteAdmin { get; set; }
        public string BGColor { get; set; }
        public string FontColor { get; set; }
        public string Initial { get; set; }
        public string SaveStatus { get; set; }
        public string EntryOrigin { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }        

    }
}