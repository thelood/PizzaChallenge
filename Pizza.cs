using System.Collections.Generic;

namespace PizzaChallenge
{


    public class Margherita : IPizza
    {
    public List<string> ingredients { get; set; }
        
    }

public class Funghi : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Capricciosa : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class QuattroStagioni : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Vegetariana : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class QuattroFormaggi : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Marinara : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Peperoni : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Napolitana : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Hawaii : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Calzone : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Rucola : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Bolognese : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class MeatFeast : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Kebabpizza : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Mexicana : IPizza
    {
    public List<string> ingredients { get; set; }
}

public class Pizza 
    {
    public Margherita margherita { get; set; }
    public int price { get; set; }
    public Funghi funghi { get; set; }
    public string nil { get; set; }
    public Capricciosa capricciosa { get; set; }
    public QuattroStagioni quattro_stagioni { get; set; }
    public Vegetariana vegetariana { get; set; }
    public QuattroFormaggi quattro_formaggi { get; set; }
    public Marinara marinara { get; set; }
    public Peperoni peperoni { get; set; }
    public Napolitana napolitana { get; set; }
    public Hawaii hawaii { get; set; }
    public Calzone calzone { get; set; }
    public Rucola rucola { get; set; }
    public Bolognese bolognese { get; set; }
    public MeatFeast meat_feast { get; set; }
    public Kebabpizza kebabpizza { get; set; }
    public Mexicana mexicana { get; set; }
}

public class RootObjectPizza
{
    public IEnumerable<Pizza> pizzas { get; set; }
}
}
