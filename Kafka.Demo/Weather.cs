public record Weather(string state, int Temparature)
{
    public override string ToString()
    {
        return $"the {nameof(Temparature)} is : {Temparature} and the {nameof(state)} is {state}";
    }
}