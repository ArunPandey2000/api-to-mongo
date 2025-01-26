using Newtonsoft.Json;

namespace test.Models
{
    public class ApiResponse
    {
        public Data data { get; set; } = null!;
    }

    public class Data
    {
        public Actor actor { get; set; } = null!;
    }

    public class Actor
    {
        public Account account { get; set; } = null!;
        public User user { get; set; } = null!;
    }

    public class Account
    {
        public Nrql nrql { get; set; } = null!;
    }

    public class Nrql
    {
        public List<Result> results { get; set; } = null!;
    }

    public class Result
    {
        public string facet { get; set; } = string.Empty;
        public int count { get; set; }

        [JsonProperty("error.message")]
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class User
    {
        public string name { get; set; } = string.Empty;
    }
}
