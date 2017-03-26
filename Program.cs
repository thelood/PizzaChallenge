using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PizzaChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Lists and var
            List<Pizza> pizzaList = new List<Pizza>();

            List<Pizza> pizzaMeat = new List<Pizza>();
            List<Pizza> pizzaCheese = new List<Pizza>();
            List<Pizza> pizzaMeatOlives = new List<Pizza>();
            List<Pizza> pizzaMozzarelaMushrooms = new List<Pizza>();

            Pizza meatCheapest = new Pizza();
            Pizza cheeseCheapest = new Pizza();
            Pizza meatOlivaCheapest = new Pizza();
            Pizza mozzarelaMushroomsCheapest = new Pizza();

            string meatPercentage = "";
            string cheesePercentage = "";
            string meatOlivaPercentage = "";
            string mozzarelaMushroomsPercentage = "";
            #endregion

            #region Read from file & parsing pizzas

            using (StreamReader r = new StreamReader("pizzaDataBase.json"))
            {
                string fromFile = r.ReadToEnd();

                RootObjectPizza rootPizza = JsonConvert.DeserializeObject<RootObjectPizza>(fromFile);
                
                foreach (var pizza in rootPizza.pizzas)
                {
                    // Add every pizza in list of all pizzas
                    pizzaList.Add(pizza);

                    var pizzaProperties = pizza.GetType().GetProperties().Where(p => typeof(IPizza).IsAssignableFrom(p.PropertyType));

                    foreach (var pizzaType in pizzaProperties)
                    {
                        var p = pizzaType.GetValue(pizza);
                        if (p != null)
                        {
                            var ingredients = ((IPizza)p).ingredients;

                            // Group 1 - only meat NO seafood and fish
                            if (ingredients.Contains("ham") || ingredients.Contains("cocktail_sausages") || ingredients.Contains("salami") || ingredients.Contains("minced_meat") || ingredients.Contains("sausage") || ingredients.Contains("kebab") || ingredients.Contains("minced_beef"))
                            {
                                pizzaMeat.Add(pizza);

                                // Group 3 - meat and olives
                                if (ingredients.Contains("olives") || ingredients.Contains("black_olives") || ingredients.Contains("green_olives"))
                                    pizzaMeatOlives.Add(pizza);
                            }
                            
                            // Group 2 - at least 2 types of cheese - mozzarela, mozzarela_cheesee, parmesan_cheese, black_cheese, goat_cheese
                            int counter = 0;
                            foreach(string s in ingredients)
                            {
                                if ( (s.Equals ("mozzarella") || s.Equals("mozzarella_cheese")) || s.Equals("parmesan_cheese") || s.Equals("black_cheese") || s.Equals("goat_cheese"))
                                    counter++;
                            }

                            if (counter > 1)
                                pizzaCheese.Add(pizza);

                            // Group 4 - mushroooms and mozzarela
                            if (ingredients.Contains("mushrooms") && (ingredients.Contains("mozzarella_cheese") || ingredients.Contains("mozzarella")))
                                pizzaMozzarelaMushrooms.Add(pizza);

                        }

                    }
                }
            }
            #endregion

            #region Calculating cheapest pizzas and percentages for each group

            meatCheapest = FindCheapest(pizzaMeat);
            cheeseCheapest = FindCheapest(pizzaCheese);
            meatOlivaCheapest = FindCheapest(pizzaMeatOlives);
            mozzarelaMushroomsCheapest = FindCheapest(pizzaMozzarelaMushrooms);

            meatPercentage = GetPercentage(pizzaMeat.Count, pizzaList.Count);
            cheesePercentage = GetPercentage(pizzaCheese.Count, pizzaList.Count);
            meatOlivaPercentage = GetPercentage(pizzaMeatOlives.Count, pizzaList.Count);
            mozzarelaMushroomsPercentage = GetPercentage(pizzaMozzarelaMushrooms.Count, pizzaList.Count);
            #endregion

            #region Serialization

            Group1 g1 = new Group1 { cheapest = GetPizza(meatCheapest), percentage = meatPercentage };
            
            Group2 g2 = new Group2 { cheapest = GetPizza(cheeseCheapest), percentage = cheesePercentage };

            Group3 g3 = new Group3 { cheapest = GetPizza(meatOlivaCheapest), percentage = meatOlivaPercentage };

            Group4 g4 = new Group4 { cheapest = GetPizza(mozzarelaMushroomsCheapest), percentage = mozzarelaMushroomsPercentage };

            PersonalInfo person = new PersonalInfo { full_name = "David Djekic", email = "david.djekic@gmail.com", code_link = "https://github.com/thelood/PizzaChallenge" };

            Answer a = new Answer { group_1 = g1, group_2 = g2, group_3 = g3, group_4 = g4 };

            RootObjectPerson rootPerson = new RootObjectPerson();
            rootPerson.personal_info = person;
            rootPerson.answer = new List<Answer>();
            rootPerson.answer.Add(a);

            string toFile = JsonConvert.SerializeObject(rootPerson, Formatting.Indented);

            File.WriteAllText("renderedText.json",toFile);
            #endregion

            string renderedTextUrl = "http://coding-challenge.renderedtext.com/submit";
            SendFile(toFile, renderedTextUrl);

            Console.ReadKey();
        }

        // Calculates percentage for every group
        #region GetPercentage
        public static string GetPercentage(int a,int b)
        {
            return (((double)a / b) * 100).ToString("#.##");
        }
        #endregion

        // Gets cheapest pizza for every group
        #region FindCheapest
        public static Pizza FindCheapest(List<Pizza> pizzas)
        {
            return pizzas.Aggregate((c1, c2) => c1.price < c2.price ? c1 : c2);
        }
        #endregion

        // Gets pizza with price and ingredients
        #region GetPizza
        public static string GetPizza(Pizza pizza)
        {
            string result = "";
            
            var pizzaPropertiesCheapest = pizza.GetType().GetProperties().Where(p => typeof(IPizza).IsAssignableFrom(p.PropertyType));
            foreach (var pizzaType in pizzaPropertiesCheapest)
            {
                var p1 = pizzaType.GetValue(pizza);
                if (p1 != null)
                {
                    var ingredients = ((IPizza)p1).ingredients;
                    result = p1.GetType().Name + " " + pizza.price.ToString() + " "  + String.Join(" ", ingredients.ToArray());   
                }
            }

            return result;
        }
        #endregion

        // Send file
        #region Send file via HTTP POST request 
        public static void SendFile(string file, string url)
        {
            
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://posttestserver.com/post.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(file);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
        #endregion
    }

}
