using EsoCPU;

namespace EsoCPU.Instructions.Flipbit;

public class LBL : IDefinition
{
  public string Token => "LBL";
  public List<ParameterConstraint> ParameterConstraints => new() { ParameterConstraint.Constant };
  public Func<ICPU, Instruction, bool> Func => (ICPU cpu, Instruction instruction) => true;
}

public class FLIPBIT : IDefinition
{
  public string Token => "FLIPBIT";

  public List<ParameterConstraint> ParameterConstraints => new() { ParameterConstraint.Memory, ParameterConstraint.Constant };

  public Func<ICPU, Instruction, bool> Func => (ICPU cpu, Instruction instruction) =>
  {
    var p1 = cpu.GetParameterValue(instruction.Parameters[0], out var p1Value);
    var p2 = cpu.GetParameterValue(instruction.Parameters[1], out var p2Value);
    if (!p1 || !p2) return false;

    var value = p1Value == 1 ? 0 : 1;
    cpu.SetParameterValue(instruction.Parameters[0], value);

    var idx = cpu.Instructions.FindIndex((v) => v.Definition.Token == "LBL" && v.Parameters[0].Value == p2Value);
    if (idx == -1) return false;
    cpu.SetPointer(idx);
    return true;
  };
}