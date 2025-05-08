using EncodeCalculator.ProjectManager.Enum;

namespace EncodeCalculator.ProjectManager.Entry;

public class ProjectEntry
{
    public string ProjectName { get; set; }
    public ProjectType ProjectType { get; set; } = ProjectType.VariableCalculator;
}