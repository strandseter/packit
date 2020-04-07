
namespace Packit.Model.Models
{
    public interface IDatabase //TODO: Fix naming, interface and method
    {
        int GetId();
        void SetId(int id);
        User User { get; set; }
    }
}
