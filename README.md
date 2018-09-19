# Product Price Combinator
Combines products by their price to archive exact total price for given minimum amout of unique products.

> This is created for a children school mathematics exercise solving tool. Nothing scientific about it. Just randomly generates product list and checks result against input rules. Gets the job done.

## Input
* **MinProductCount** - Minimum amout of unique products that has to be in the output list
* **TargetPrice** - Target price to archive by adding all the products in the list
* **ProductPrices** - Product names and prices as `IDictionary<string, decimal>` (Key - product name, Value - product price)

## Output
* List of product names and product counts

## Example

### Input
```
int MinProductCount = 6;
decimal TargetPrice = 10.0m;
TimeSpan MaxCalculationTime = TimeSpan.FromSeconds(5);
Dictionary<string, decimal> ProductPrices = new Dictionary<string, decimal>
{
    { "Estrella krõpsud", 1.53m },
    { "Kõrremahl", 0.32m },
    { "Pulgakomm", 0.13m },
    { "Twixi batoon", 0.38m },
    { "Geisha shokolaad", 1.15m },
    { "Väikese Tomi jäätis", 0.51m },
    { "Kellukese limonaad", 0.77m },
    { "Orbiti näts", 0.45m },
};
```

### Executing
```
var finder = new CombinationFinder(ProductPrices, TargetPrice, MinProductCount);

Console.WriteLine("Start");

var sw = new Stopwatch();
sw.Start();
finder.FindBestCombination(MaxCalculationTime);
sw.Stop();

Console.WriteLine($"Best result:");
finder.OutputCurrentBestResult();
Console.WriteLine($"End ({sw.Elapsed})");
```

### Output
```
Start
11 (5/1,8): 'Estrella krõpsud'x1, 'Kõrremahl'x2, 'Pulgakomm'x1, 'Twixi batoon'x5, 'Geisha shokolaad'x0, 'Väikese Tomi jäätis'x0, 'Kellukese limonaad'x1, 'Orbiti näts'x1
11 (5/1,8): 'Estrella krõpsud'x1, 'Kõrremahl'x5, 'Pulgakomm'x1, 'Twixi batoon'x2, 'Geisha shokolaad'x0, 'Väikese Tomi jäätis'x1, 'Kellukese limonaad'x1, 'Orbiti näts'x0
11 (5/1,8): 'Estrella krõpsud'x1, 'Kõrremahl'x1, 'Pulgakomm'x2, 'Twixi batoon'x5, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x0, 'Kellukese limonaad'x0, 'Orbiti näts'x1
11 (4/1,8): 'Estrella krõpsud'x2, 'Kõrremahl'x2, 'Pulgakomm'x1, 'Twixi batoon'x1, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x4, 'Kellukese limonaad'x0, 'Orbiti näts'x0
10 (4/1,7): 'Estrella krõpsud'x4, 'Kõrremahl'x0, 'Pulgakomm'x2, 'Twixi batoon'x1, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x1, 'Kellukese limonaad'x1, 'Orbiti näts'x0
10 (3/1,7): 'Estrella krõpsud'x3, 'Kõrremahl'x0, 'Pulgakomm'x3, 'Twixi batoon'x1, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x1, 'Kellukese limonaad'x1, 'Orbiti näts'x0
10 (3/1,7): 'Estrella krõpsud'x3, 'Kõrremahl'x1, 'Pulgakomm'x0, 'Twixi batoon'x1, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x1, 'Kellukese limonaad'x0, 'Orbiti näts'x3
10 (3/1,7): 'Estrella krõpsud'x3, 'Kõrremahl'x3, 'Pulgakomm'x1, 'Twixi batoon'x1, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x0, 'Kellukese limonaad'x1, 'Orbiti näts'x0
10 (3/1,7): 'Estrella krõpsud'x3, 'Kõrremahl'x1, 'Pulgakomm'x1, 'Twixi batoon'x0, 'Geisha shokolaad'x1, 'Väikese Tomi jäätis'x3, 'Kellukese limonaad'x0, 'Orbiti näts'x1
10 (3/1,7): 'Estrella krõpsud'x1, 'Kõrremahl'x1, 'Pulgakomm'x0, 'Twixi batoon'x1, 'Geisha shokolaad'x3, 'Väikese Tomi jäätis'x3, 'Kellukese limonaad'x1, 'Orbiti näts'x0
Best result:
10 (3/1,7): 'Estrella krõpsud'x1, 'Kõrremahl'x1, 'Pulgakomm'x0, 'Twixi batoon'x1, 'Geisha shokolaad'x3, 'Väikese Tomi jäätis'x3, 'Kellukese limonaad'x1, 'Orbiti näts'x0
End (00:00:05.0068061)
```