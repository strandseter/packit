
namespace Packit.Model.Models
{
    public interface IDatabase
    {
        int Id { get; set; }
        User User { get; set; }
    }
}
