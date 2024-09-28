using EsoCPU;

namespace EsoCPU.Instructions.Subleq;

public class LBL : IDefinition
{
  public string Token => "LBL";
  public List<ParameterConstraint> ParameterConstraints => new() { ParameterConstraint.Constant };
  public Func<ICPU, Instruction, bool> Func => (ICPU cpu, Instruction instruction) => true;
}

public class SUBLEQ : IDefinition
{
  public string Token => "SUBLEQ";

  public List<ParameterConstraint> ParameterConstraints => new() { ParameterConstraint.Memory, ParameterConstraint.ConstantOrMemory, ParameterConstraint.Constant };

  public Func<ICPU, Instruction, bool> Func => (ICPU cpu, Instruction instruction) =>
  {
    var p1 = cpu.GetParameterValue(instruction.Parameters[0], out var p1Value);
    var p2 = cpu.GetParameterValue(instruction.Parameters[1], out var p2Value);
    var p3 = cpu.GetParameterValue(instruction.Parameters[2], out var p3Value);
    if (!p1 || !p2 || !p3) return false;
    var value = p1Value - p2Value;
    cpu.SetParameterValue(instruction.Parameters[0], value);
    if (value <= 0)
    {
      var idx = cpu.Instructions.FindIndex(v => v.Definition.Token == "LBL" && v.Parameters[0].Value == p3Value);
      if (idx == -1) return false;
      cpu.SetPointer(idx);
      return true;
    }
    return true;
  };
}