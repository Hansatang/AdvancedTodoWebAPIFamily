using System.Collections.Generic;

namespace Models {
public class Child : Person {
    
    public List<Interest> Interests { get; set; }
    public List<Pet> Pets { get; set; }


    public string GetInterest()
    {
        string str = "";
        foreach (Interest interest in Interests)
        {
            str += interest.Type  + ", ";
        }

        return str;
    }
    
    public string GetPets()
    {
        string str = "";
        foreach (Pet pet in Pets)
        {
            str += pet.Name  + ", ";
        }

        return str;
    }
}
}