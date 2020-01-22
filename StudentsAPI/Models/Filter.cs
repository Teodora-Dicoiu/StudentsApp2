using System.Text.Json.Serialization;

namespace StudentsAPI.Models
{
    public class Filter
    {
        public FilterType Type { get; set; }

        public string[] Values { get; set; }

        public Field Field { get; set; }
    }

    public enum FilterType
    {
        None,
        Equals,
        Contains,
        StartsWith,
        EndsWith
    }

    public enum Field
    {
        Name,
        Email,
        Phone
    }
}
