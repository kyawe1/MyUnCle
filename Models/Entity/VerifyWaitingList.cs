using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UncleApp.Models.Entity;

public class VerifyWaitingList{
    public int Id{set;get;}
    [ForeignKey("UserId")]
    public string UserId{set;get;}
    public IdentityUser User{set;get;}
    public DateTime Created{set;get;}=DateTime.UtcNow;
    public DateTime Updated{set;get;}
    public bool waiting {set;get;}=true;
}