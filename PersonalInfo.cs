using System.Collections.Generic;

namespace PizzaChallenge
{
    public class PersonalInfo
{
    public string full_name { get; set; }
    public string email { get; set; }
    public string code_link { get; set; }
}

public class Group1
{
    public string percentage { get; set; }
    public string cheapest { get; set; }
}

public class Group2
{
    public string percentage { get; set; }
    public string cheapest { get; set; }
}

public class Group3
{
    public string percentage { get; set; }
    public string cheapest { get; set; }
}

public class Group4
{
    public string percentage { get; set; }
    public string cheapest { get; set; }
}

public class Answer
{
    public Group1 group_1 { get; set; }
    public Group2 group_2 { get; set; }
    public Group3 group_3 { get; set; }
    public Group4 group_4 { get; set; }
}

public class RootObjectPerson
{
    public PersonalInfo personal_info { get; set; }
    public List<Answer> answer { get; set; }
}
}
