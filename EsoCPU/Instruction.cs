namespace EsoCPU;

public class Instruction
{
    public IDefinition Definition { get; set; }
    public List<Parameter> Parameters = new();

    public Instruction(IDefinition definition, List<Parameter> parameters)
    {
        Definition = definition;
        Parameters = parameters;
    }
}