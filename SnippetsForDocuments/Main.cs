using System.Collections.Generic;

public static IEnumerable<TResult> Select<T, TResult>(
    this IEnumerable<T> items, 
    Func<T, TResult> selector)
{
    foreach (var item in items)
    {
        yield return selector(item);
    }
}

public static IEnumerable<T> Where<T>(
    this IEnumerable<T> items, 
    Func<T, bool> predicate)
{
    foreach (var item in items)
    {
        if (predicate(item))
        {
            yield return item;
        }
    }
}


interface IAnimal {}

class Giraffe: IAnimal {}
class Whale: IAnimal {}

interface IProcessor<in T>  
{  
    void Process(IEnumerable<T> ts);  
}

List<Giraffe> giraffes = new List<Giraffe> { new Giraffe() };  
List<Whale> whales = new List<Whale> { new Whale() };

IProcessor<IAnimal> animalProc = new Processor<IAnimal>();  
IProcessor<Giraffe> giraffeProcessor = animalProc;  
IProcessor<Whale> whaleProcessor = animalProc;  

giraffeProcessor.Process(giraffes);  
whaleProcessor.Process(whales);  
