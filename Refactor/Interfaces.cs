//Use this interface if you want to make sure you copy a class and not just reference it
public interface IClonable<T> where T : class //Just to make sure T is a class
{
    T Clone();  //Clone method
}

//Use this interface if you want to use an item
public interface MainAction
{
    void Use(object[] parameters); //Use method
}

public interface SecondAction
{
    void DoSecondAction();
}

public interface CanEffect
{
    void ApplyEffect(Health health);
}

//Used for goals to trigger something in the map
public interface IActionable
{
    void Action();
}
