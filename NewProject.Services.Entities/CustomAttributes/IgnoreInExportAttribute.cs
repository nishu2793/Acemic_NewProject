
namespace NewProject.Services.Entities.CustomAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class IgnoreInExportAttribute : Attribute
    {
        //Class Members
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class IsExcelAttribute : Attribute
    {
        //Class Members
    }
}
