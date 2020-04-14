
namespace Packit.Model.Models
{
    public interface IDatabase //TODO: Fix naming, interface and methods
    {
        int Id { get; set; }
        User User { get; set; }
    }
}
