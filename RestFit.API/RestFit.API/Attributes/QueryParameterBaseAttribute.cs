using Microsoft.OpenApi.Models;

namespace RestFit.API.Attributes
{
    public abstract class QueryParameterBaseAttribute : Attribute
    {
        public string? Name { get; private set; }
        public string? DataType { get; set; }
        public string? Format { get; set; }
        public ParameterLocation ParameterType { get; set; }
        public string Description { get; private set; }
        public bool Required { get; set; }

        protected QueryParameterBaseAttribute(string name, string description) : this(description)
        {
            Name = name;
        }

        protected QueryParameterBaseAttribute(string description)
        {
            Description = description;
        }
    }
}
