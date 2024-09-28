namespace EsoCPU;

public enum ParseResultComment
{
    OK,
    ERROR,
    NO_CODE,
    INVALID_INSTRUCTION,
    INVALID_PARAMETERS,
    INVALID_PARAMETER
}

public enum StepResultComment
{
    OK,
    ERROR,
    END_OF_CODE,
    NO_INSTRUCTION
}

public class Result<T> where T : Enum
{
    public bool Success { get; set; }
    public T Comment { get; set; }
    public int Line { get; set; }

    public Result(bool success, T comment, int line)
    {
        Success = success;
        Comment = comment;
        Line = line;
    }

}
