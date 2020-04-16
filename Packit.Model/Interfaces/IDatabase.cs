
namespace Packit.Model.Models
{
    public interface IDatabase
    {
        int GetId();
        void SetId(int value);
        User User { get; set; }
    }
}
