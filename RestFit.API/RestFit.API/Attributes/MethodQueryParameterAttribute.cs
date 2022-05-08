namespace RestFit.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class MethodQueryParameterAttribute : QueryParameterBaseAttribute
    {
        public MethodQueryParameterAttribute(string name, string description) : base(name, description)
        {
        }
    }
}
