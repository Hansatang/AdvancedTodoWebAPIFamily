using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models {
public class Family {
    
    public int Id { get; set; }
    public string StreetName { get; set; }
    public int HouseNumber{ get; set; }
    public List<Adult> Adults { get; set; }
    public List<Child> Children{ get; set; }
    public List<Pet> Pets{ get; set; }
    

    public string GetAdults()
    {
        string str = "";
        foreach (Adult adult in Adults)
        {
            str +=  adult.FirstName + " " + adult.LastName + ", ";
        }

        return str;
    }
    
    public string GetChildren()
    {
        if (Children != null)
        {
             string str = "";
                    foreach (Child child in Children)
                    {
                        str +=  child.FirstName + " " + child.LastName + ", ";
                    }
            
                    return str;
        }
        else
        {
            return "No children";
        }
    }
    
    public string GetPets()
    {
        if (Pets != null)
        {
            string str = "";
            foreach (Pet pet in Pets)
            {
                str += pet.Name + ", ";
            }

            return str;
        }
        else
        {
            return "No Pets";
        }
    }
}



}