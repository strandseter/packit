using Packit.Model;

namespace Packit.App
{
    public static class CurrentUserStorage
    {
        public static User User { get; set; } = new User() { JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE0IiwiaWQiOiIxNCIsIm5iZiI6MTU5MDU5NzAzMCwiZXhwIjoxNjE2NTE3MDMwLCJpYXQiOjE1OTA1OTcwMzB9.buUexbiPrivQqr4EosaU6l54p6JddoI8cET9-p9vrfs"};
    }
}
