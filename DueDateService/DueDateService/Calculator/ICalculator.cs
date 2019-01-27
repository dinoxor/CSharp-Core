using DueDateService.Model;

namespace DueDateService.Calculator
{
    internal interface ICalculator
    {
        DueDateResponse Calculate(DueDateRequest request);
    }
}