namespace Day17;

public class Operation
{
    public int Opcode { get; set; }

    public int Operand { get; set; }

    public Operation(int opcode, int operand, Computer computer)
    {
        Opcode = opcode;
        Operand = operand;
    }

    public int GetCombo(Computer computer)
    {
        var result = Operand;
        switch (Operand)
        {
            case 4:
                result = computer.A;
                break;
            case 5:
                result = computer.B;
                break;
            case 6:
                result = computer.C;
                break;
            default:
                break;
        }
        return result;
    }

    public int Evaluate(Computer computer, int pointer)
    {
        if (Opcode == 0)
        {
            computer.A = computer.A >> GetCombo(computer);
        }
        if (Opcode == 1)
        {
            computer.B = computer.B ^ Operand;
        }
        if (Opcode == 2)
        {
            computer.B = GetCombo(computer) % 8;
        }
        if (Opcode == 3)
        {
            if (computer.A != 0)
                pointer = Operand - 2;
        }
        if (Opcode == 4)
        {
            computer.B = computer.B ^ computer.C;
        }
        if (Opcode == 5)
        {
            var temp = GetCombo(computer) % 8;
            computer.Output.Add(temp);
        }
        if (Opcode == 6)
        {
            computer.B = computer.A >> GetCombo(computer);
        }
        if (Opcode == 7)
        {
            computer.C = computer.A >> GetCombo(computer);
        }
        return pointer + 2;
    }
}
