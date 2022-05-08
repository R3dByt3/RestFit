using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestFit.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void IncludeClassXmlDocs(this SwaggerGenOptions swaggerOptions)
        {
            Directory.GetFiles(AppContext.BaseDirectory, "*.xml")
                .Where(xmlFile => File.Exists(Path.ChangeExtension(xmlFile, "dll")))
                .ToList()
                .ForEach(file => swaggerOptions.IncludeXmlComments(file));
        }
    }
}
